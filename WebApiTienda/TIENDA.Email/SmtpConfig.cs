using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Email
{
    public class SmtpConfig
    {
        public string HostName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool SslEnabled { get; set; }
        public int TimeOut { get; set; }
        public int Port { get; set; }

        public string DefaultEmailFrom { get; set; }
        public string DefaultNameFrom { get; set; }
    }
}
