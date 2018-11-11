using System;

namespace XemsMailer.Exceptions
{
    public class SendingFailException : Exception
    {
        public SendingFailException(string message) : 
            base(message) { }
    }
}