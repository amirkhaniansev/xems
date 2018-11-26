using System;

namespace AuthTokenService
{
    /// <summary>
    /// Class for Token event argument
    /// </summary>
    public class TokenEventArgs:EventArgs
    {
        /// <summary>
        /// Access token
        /// </summary>
        private readonly string _accessToken;

        /// <summary>
        /// Refresh Token
        /// </summary>
        private readonly string _refreshToken;

        /// <summary>
        /// Gets access token
        /// </summary>
        public string AccessToken => this._accessToken;

        /// <summary>
        /// Gets refresh token
        /// </summary>
        public string RefreshToken => this._refreshToken;

        /// <summary>
        /// Creates new instance of <see cref="TokenEventArgs"/>
        /// </summary>
        /// <param name="accessToken">Access token</param>
        /// <param name="refreshToken">Refresh token</param>
        public TokenEventArgs(string accessToken,string refreshToken)
        {
            // setting fields
            this._accessToken = accessToken;
            this._refreshToken = refreshToken;
        }

    }
}
