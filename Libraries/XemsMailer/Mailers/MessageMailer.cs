using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using XemsMailer.Base;

namespace XemsMailer.Mailers
{
    public class MessageMailer : MailerSenderBase<string>
    {
        public MessageMailer(NetworkCredential credentials,string address) : 
            base(credentials,address) {}

        public override void Send(string to, string subject, bool IsBodyHtml, string info)
        {
            try
            {
                var mail = this.ConstructMail(to, subject, IsBodyHtml);

                this._smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                // logging...
                throw ex;
            }
        }

        public override void Send(IEnumerable<string> to, string subject, bool isBodyHtml, string info)
        {
            foreach (var receiver in to)
            {
                this.Send(receiver,subject,isBodyHtml,info);
            }
        }

        public override Task SendAsync(string to, string subject, bool isBodyHtml, string info) =>
            Task.Run(() => this.Send(to, subject, isBodyHtml, info));

        public override Task SendAsync(IEnumerable<string> to, string subject, bool isBodyHtml, string info) =>
            Task.Run(() => this.Send(to, subject, isBodyHtml, info));
    }
}