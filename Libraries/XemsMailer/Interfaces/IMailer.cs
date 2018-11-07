using System.Collections.Generic;
using System.Threading.Tasks;

namespace XemsMailer.Interfaces
{
    /// <summary>
    /// Interface fro mailer
    /// </summary>
    /// <typeparam name="TInfo"></typeparam>
    public interface IMailer<in TInfo>
    {
        /// <summary>
        /// Sends information via email
        /// </summary>
        /// <param name="to">Email address to which e-mail will be sent.</param>
        /// <param name="info">Information that will be sent.</param>
        /// <param name="subject">Subject of email.</param>
        /// <param name="IsBodyHtml"> Boolean value indicating whether the email has HTML body.</param>
        void Send(string to, string subject, bool IsBodyHtml, TInfo info);

        /// <summary>
        /// Sends information via email
        /// </summary>
        /// <param name="to">Email addresses to which e-mail will be sent.</param>
        /// <param name="info">Information that will be sent.</param>
        /// <param name="subject">Subject of email.</param>
        /// <param name="IsBodyHtml"> Boolean value indicating whether the email has HTML body.</param>
        void Send(IEnumerable<string> to, string subject, bool IsBodyHtml, TInfo info);

        /// <summary>
        /// Sends information via email asynchronously
        /// </summary>
        /// <param name="to">Email address to which e-mail will be sent.</param>
        /// <param name="info">Information that will be sent.</param>
        /// <returns>task</returns>
        /// <param name="subject">Subject of email.</param>
        /// <param name="IsBodyHtml"> Boolean value indicating whether the email has HTML body.</param>
        Task SendAsync(string to, string subject, bool IsBodyHtml, TInfo info);

        /// <summary>
        /// Sends information via email
        /// </summary>
        /// <param name="to">Email addresses to which e-mail will be sent.</param>
        /// <param name="info">Information that will be sent.</param>
        /// <remarks>task</remarks>
        /// <param name="subject">Subject of email.</param>
        /// <param name="IsBodyHtml"> Boolean value indicating whether the email has HTML body.</param>
        Task SendAsync(IEnumerable<string> to, string subject, bool IsBodyHtml, TInfo info);
    }
}