using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KevinColemanInc.Utilities
{
    public static class MailUtil
    {

        public static void SendEmail(string email, string subject, string body)
        {
            string key = ConfigurationManager.AppSettings["mandrilKey"];
            string jsonEmail = "{\"key\":\""+key+"\",\"message\": {\"text\": \"" + body + "\",\"subject\": \"" + subject + "\",\"from_email\": \"BitcoinBot@BitcoinFTW.com\",\"from_name\": \"bitcoinFTW\",\"to\": [{\"email\": \""+email+"\",\"name\": \"Kevin's Gmail\"},{\"email\": \"4074531225@txt.att.net\",\"name\": \"Kevin's phone\"}]},\"async\": true}";

            StringBuilder html = WebUtil.HTTPPost("https://mandrillapp.com/api/1.0/messages/send.json", jsonEmail);
        }
    }
}
