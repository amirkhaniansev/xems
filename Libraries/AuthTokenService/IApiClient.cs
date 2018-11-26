using System;

namespace AuthTokenService
{
    public interface IApiClient : IDisposable
    {
        void UpdateToken(object sender, TokenEventArgs eventArgs);
    }
}