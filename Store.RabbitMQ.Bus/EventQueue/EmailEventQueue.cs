using Store.RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.RabbitMQ.Bus.EventQueue
{
    public class EmailEventQueue : Event
    {
        public string Addressee { get; set; }

        public string Title { get; set; }

        public string Conent { get; set; }

        public EmailEventQueue(string addresse, string title, string content)
        {
            Addressee = addresse;
            Title = title;
            Conent = content;
        }
    }
}
