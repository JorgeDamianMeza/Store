using SendGrid;
using SendGrid.Helpers.Mail;
using Store.Messenger.Email.SendGridLibrary.Interface;
using Store.Messenger.Email.SendGridLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Messenger.Email.SendGridLibrary.Implement
{
    public class SendGrid : ISendGrid
    {
        public async Task<(bool result, string message)> SendEmail(SendGridData sendGridData)
        {

            try
            {
                var sendGridClient = new SendGridClient(sendGridData.SendGridAPIKey);
                var adressee = new EmailAddress(sendGridData.EmailAddressee, sendGridData.NameAddressee);
                var titleEmial = sendGridData.Title;
                var sender = new EmailAddress("naimad.xrz@hotmail.com", "Damian Meza");
                var content = sendGridData.Content;

                var objMessage = MailHelper.CreateSingleEmail(sender, adressee, titleEmial, content, content);

                await sendGridClient.SendEmailAsync(objMessage);

                return (true, null);
            }catch(Exception ex)
            {
                return (false, ex.Message);
            }

           
        }
    }
}
