using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using XemsMailer.Base;

namespace XemsMailer.Mailers
{
    public class QrMailer : MailerSenderBase<Bitmap>
    {
        public QrMailer(NetworkCredential credentials, string address) : 
            base(credentials, address)
        {
        }

        public override void Send(string to, string subject, bool isBodyHtml, Bitmap qrImage)
        {
            try
            {
                var mail = this.ConstructMail(to, subject, isBodyHtml);

                var stream = new MemoryStream();

                qrImage.Save(stream, ImageFormat.Jpeg);

                stream.Position = 0;

                var img = new LinkedResource(stream, "image/jpeg")
                {
                    ContentId = "recipe_qr_code"
                };

                var foot = AlternateView.CreateAlternateViewFromString("<p> <img src=cid:recipe_qr_code /> </p>", null, "text/html");

                foot.LinkedResources.Add(img);

                mail.AlternateViews.Add(foot);

                mail.To.Add(to);

                this._smtpClient.Send(mail);

                mail.Dispose();
                foot.Dispose();
                img.Dispose();
                stream.Dispose();
                qrImage.Dispose();
            }
            catch (Exception ex)
            {
                // logging...
                throw ex;
            }
        }

        public override void Send(IEnumerable<string> to, string subject, bool isBodyHtml, Bitmap info)
        {
            foreach (var receiver in to)
            {
                this.Send(receiver,subject,isBodyHtml,info);
            }
        }

        public override Task SendAsync(string to, string subject, bool isBodyHtml, Bitmap info) =>
            Task.Run(() => this.Send(to, subject, isBodyHtml, info));

        public override Task SendAsync(IEnumerable<string> to, string subject, bool isBodyHtml, Bitmap info) =>
            Task.Run(() => this.Send(to, subject, isBodyHtml, info));
    }
}