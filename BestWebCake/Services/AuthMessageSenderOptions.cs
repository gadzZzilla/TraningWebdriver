namespace BestWebCake.Services
{
    using System;
    public class AuthMessageSenderOptions
    {
        public string SendGridKey => new Guid().ToString();
    }
}