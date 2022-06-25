using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Messenger.Email.SendGridLibrary.Model
{
    public class SendGridData
    {
        public string SendGridAPIKey { get; set; }
        public string EmailAddressee { get; set; }
        public string NameAddressee { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
