using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Email
{
    public class MailModel
    {
        public MailAddress  From { get; set; }
        public List<EmailAddress> To { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsBodyHtml { get; set; } = true;
        public List<string> AttachmendFiles { get; set; }
    }
}
