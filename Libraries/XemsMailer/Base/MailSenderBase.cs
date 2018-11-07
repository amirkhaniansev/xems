using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using XemsMailer.Interfaces;

namespace XemsMailer.Base
{
    public abstract class MailerSenderBase<TInfo> : IMailer<TInfo>
    {
        private readonly NetworkCredential _credentials;

        private readonly SmtpClient _smtpClient;

        private readonly string _host;

        private readonly MailAddress _address;

        private readonly int _port;

        private readonly bool _enableSsl;

        private readonly SmtpDeliveryMethod _deliveryMethod;

        public string Host => this._host;

        public MailAddress FromAddress => this._address;

        public int Port => this._port;

        public bool EnableSsl => this._enableSsl;

        public SmtpDeliveryMethod SmtpDeliveryMethod => this._deliveryMethod;

        public MailerSenderBase(NetworkCredential credentials,
            string address,
            string host = "smtp.google.com",
            int port = 587,
            bool enableSsl = true,
            SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network)
        {
            this._credentials = credentials;
            this._host = host;
            this._port = port;
            this._enableSsl = enableSsl;
            this._deliveryMethod = deliveryMethod;

            this._address = new MailAddress(address);

            this._smtpClient = new SmtpClient
            {
                Host = this._host,
                Port = this._port,
                Credentials = this._credentials,
                EnableSsl = this._enableSsl,
                DeliveryMethod = this._deliveryMethod
            };
        }

        public abstract void Send(string to, string subject, bool IsBodyHtml, TInfo info);

        public abstract void Send(IEnumerable<string> to, string subject, bool IsBodyHtml, TInfo info);

        public abstract Task SendAsync(string to, string subject, bool IsBodyHtml, TInfo info);

        public abstract Task SendAsync(IEnumerable<string> to, string subject, bool IsBodyHtml, TInfo info);
        
        protected MailMessage ConstructMail(string to, string subject, bool isBodyHtml)
        {
            if(string.IsNullOrEmpty(to))
                throw new ArgumentNullException("to");

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject");

            var mail = new MailMessage
            {
                From = this._address,
                Subject = subject,
                IsBodyHtml = isBodyHtml
            };

            mail.To.Add(to);

            return mail;
        }
    }
}