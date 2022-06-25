using Store.RabbitMQ.Bus.Commands;
using Store.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.RabbitMQ.Bus.BusRabbit
{
    public interface IRabbitEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T evento) where T : Event;

        void Subscribe<T, TH>() where T : Event
                                where TH : IEventHandler<T>;
    }
}
