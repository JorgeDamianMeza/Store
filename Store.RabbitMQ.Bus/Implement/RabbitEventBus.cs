using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Store.RabbitMQ.Bus.BusRabbit;
using Store.RabbitMQ.Bus.Commands;
using Store.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.RabbitMQ.Bus.Implement
{
    public class RabbitEventBus : IRabbitEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _evenTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitEventBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _evenTypes = new List<Type>();
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Publish<T>(T evento) where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "rabbit-damian-web" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = evento.GetType().Name;
                channel.QueueDeclare(eventName, false, false, false, null);
                var message = JsonConvert.SerializeObject(evento);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", eventName, null, body);

            }
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerEventType = typeof(TH);

            if (!_evenTypes.Contains(typeof(T)))
            {
                _evenTypes.Add(typeof(T));
            }
            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }
            if (_handlers[eventName].Any(x => x.GetType() == handlerEventType))
            {
                throw new ArgumentException($"Handler {handlerEventType.Name} was prevously registred by {eventName}");
            }

            _handlers[eventName].Add(handlerEventType);

            var factory = new ConnectionFactory()
            {
                HostName = "rabbit-damian-web",
                DispatchConsumersAsync = true
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Delegate;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Delegate(object sender, BasicDeliverEventArgs @event)
        {
            var nameEvent = @event.RoutingKey;
            var message = Encoding.UTF8.GetString(@event.Body.ToArray());

            try
            {
                if (_handlers.ContainsKey(nameEvent))
                {
                    using(var scope = _serviceScopeFactory.CreateScope())
                    {

                        var subscriptions = _handlers[nameEvent];
                        foreach (var subscription in subscriptions)
                        {
                            var handler = scope.ServiceProvider.GetService(subscription); //Activator.CreateInstance(subscription);
                            if (handler == null) continue;

                            var typeEvent = _evenTypes.SingleOrDefault(x => x.Name == nameEvent);
                            var eventDS = JsonConvert.DeserializeObject(message, typeEvent);

                            var concreteType = typeof(IEventHandler<>).MakeGenericType(typeEvent);
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { eventDS });
                        }
                    }                  
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
