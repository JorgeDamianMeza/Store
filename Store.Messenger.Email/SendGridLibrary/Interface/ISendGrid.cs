using Store.Messenger.Email.SendGridLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Messenger.Email.SendGridLibrary.Interface
{
    public interface ISendGrid
    {
        Task<(bool result, string message)> SendEmail(SendGridData sendGridData);
    }
}
