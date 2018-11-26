using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;

namespace AuthTokenService
{
    /// <summary>
    /// Class for retrieving tokens from Auth API.
    /// This class is also responsible for updating access tokens when they are expired or user claims are changed.
    /// </summary>
    public sealed class TokenProvider : IDisposable
    {
        /// <summary>
        /// Token client
        /// </summary>
        private readonly TokenClient _tokenClient;

        /// <summary>
        /// Token revocation client
        /// </summary>
        private readonly TokenRevocationClient _tokenRevocationClient;

        /// <summary>
        /// HTTP context
        /// </summary>
        private readonly HttpContext _httpContext;

        /// <summary>
        /// Updater
        /// </summary>
        private readonly Updater _updater;

        /// <summary>
        /// Token endpoint
        /// </summary>
        private readonly string _tokenEndpoint;

        /// <summary>
        /// Update interval in minutes
        /// </summary>
        private readonly int _updateInterval;

        /// <summary>
        /// Auth API bas address
        /// </summary>
        private readonly string _authApiAddress;

        /// <summary>
        /// Refresh token
        /// </summary>
        private string _refreshToken;

        /// <summary>
        /// Access token
        /// </summary>
        private string _accessToken;

        /// <summary>
        /// Gets refresh token
        /// </summary>
        public string RefreshToken => this._refreshToken;

        /// <summary>
        /// Gets access token
        /// </summary>
        public string AccessToken => this._accessToken;

        /// <summary>
        /// Gets Auth API address
        /// </summary>
        public string AuthApiAddress => this._authApiAddress;

        /// <summary>
        /// Gets Update interval in minutes
        /// </summary>
        public int UpdateInterval => this._updateInterval;

        /// <summary>
        /// Delegate for Token update
        /// </summary>
        /// <typeparam name="TEventArgs">Type of event argument</typeparam>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        public delegate void TokenUpdatedDelegate<TEventArgs>(object sender, TEventArgs e);

        /// <summary>
        /// Event for Token update
        /// </summary>
        public event TokenUpdatedDelegate<TokenEventArgs> TokenUpdated;

        /// <summary>
        /// Creates new instance of <see cref="TokenProvider"/>
        /// </summary>
        /// <param name="authApiAddress">Auth API base address</param>
        /// <param name="updateInterval">Update interval in minutes</param>
        public TokenProvider(string authApiAddress,int updateInterval)
        {
            // setting fields
            this._authApiAddress = authApiAddress;
            this._updateInterval = updateInterval;

           // getting token endpoint
            var disco = DiscoveryClient.GetAsync(this.AuthApiAddress).Result;

            if(disco.IsError)
            {
                throw new Exception("Auth API is not responding");
            }

            // creating clients
            this._tokenClient = new TokenClient(disco.TokenEndpoint, "DefaultClient", "secret");
            this._tokenRevocationClient = new TokenRevocationClient(disco.RevocationEndpoint, "DefaultClient", "secret");

            // creating context
            this._httpContext = new DefaultHttpContext();

            // creating updater with the given interval
            this._updater = new Updater(this._updateInterval, this.RefreshAccessTokenAsync);
        }

        /// <summary>
        /// Does Sign In operation with password credentials
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>task</returns>
        public async Task<TokenStatus> SignInAsync(string username,string password)
        {
            // getting response
            var response = await this._tokenClient.RequestResourceOwnerPasswordAsync(
                username,password,
                "offline_access UserManagementAPI MedicineAPI RecipeAPI InstitutionsAPI");
            
            if(response.IsError)
            {
                return TokenStatus.Error;
            }

            // setting tokens
            this._accessToken = response.AccessToken;
            this._refreshToken = response.RefreshToken;

            // starting update periodic process
            this._updater.StartUpdatePeriod();

            // rising token updated event
            this.RiseTokenUpdatedEvent();

            return TokenStatus.Ok;
        }

        /// <summary>
        /// Does refresh of access token
        /// </summary>
        /// <returns>task</returns>
        public async Task<TokenStatus> RefreshAccessTokenAsync()
        {
            return await this.RequestRefreshTokenAsync(this._refreshToken);
        }

        /// <summary>
        /// Checks the given refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>task</returns>
        public async Task<TokenStatus> CheckRefreshTokenAsync(string refreshToken)
        {
            // getting status
            var status = await this.RequestRefreshTokenAsync(refreshToken);

            // if refresh token is valid then assign it
            if (status == TokenStatus.Ok)
                this._refreshToken = refreshToken;

            // returning status
            return status;
        }

        /// <summary>
        /// Signs out revoking refresh token and clearing all cache from Identity server
        /// </summary>
        /// <returns>Token status</returns>
        public async Task<TokenStatus> SignOutAsync()
        {
            // revoking refresh token
            var response = await this._tokenRevocationClient.RevokeRefreshTokenAsync(this._refreshToken);

            if (response.IsError)
                return TokenStatus.Error;

            // signing out
            // which means removing authentication cache from Auth API
            // TODO
            // await this._httpContext.SignOutAsync();            

            return TokenStatus.Ok;
        }

        /// <summary>
        /// Dispose token provider
        /// </summary>
        public void Dispose()
        {
            this._updater.Dispose();
            this._tokenClient.Dispose();
            this._tokenRevocationClient.Dispose();
        }

        /// <summary>
        /// Requests new access token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Task</returns>
        private async Task<TokenStatus> RequestRefreshTokenAsync(string refreshToken)
        {
            // getting response
            var response = await this._tokenClient.RequestRefreshTokenAsync(refreshToken);

            if (response.IsError)
            {
                return TokenStatus.Error;
            }

            // setting new access token
            this._accessToken = response.AccessToken;

            // rising token updated event
            this.RiseTokenUpdatedEvent();

            return TokenStatus.Ok;
        }

        /// <summary>
        /// Rises token updated event
        /// </summary>
        private void RiseTokenUpdatedEvent()
        {
            // creating token update event argument
            var tokenUpdateEventArgs = new TokenEventArgs(this._accessToken,this._refreshToken);

            // rising token updated event
            this.TokenUpdated?.Invoke(this, tokenUpdateEventArgs);
        }
    }
}