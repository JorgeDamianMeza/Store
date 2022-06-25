using Store.Messenger.Email.SendGridLibrary.Interface;
using Store.Messenger.Email.SendGridLibrary.Model;
using Store.RabbitMQ.Bus.BusRabbit;
using Store.RabbitMQ.Bus.EventQueue;

namespace Store.Author.HandlerRabbit
{
    public class EmailEventHandler : IEventHandler<EmailEventQueue>
    {
        private readonly ILogger<EmailEventHandler> _logger;
        private readonly ISendGrid _sendGrid;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public EmailEventHandler() { }

        public EmailEventHandler(ILogger<EmailEventHandler> logger, ISendGrid sendGrid, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _logger = logger;
            _sendGrid = sendGrid;
            _configuration = configuration;
        }

        public async Task Handle(EmailEventQueue @event)
        {
            _logger.LogInformation($"This is value from rabbitMQ {@event.Title}");
            var objData = new SendGridData();
            objData.Content = @event.Conent;
            objData.EmailAddressee = @event.Addressee;
            objData.NameAddressee = @event.Addressee;
            objData.Title = @event.Title;
            objData.SendGridAPIKey = _configuration["SendGrid:ApiKey"];

            var res = await _sendGrid.SendEmail(objData);
            if (res.result) { await Task.CompletedTask; return; }

        }
    }
}
