using System;
using System.Security.Cryptography;

namespace XemsPasswordHash
{
    /// <summary>
    /// Service for password hashing
    /// </summary>
    public class PasswordHashService
    {
        /// <summary>
        /// Random number generation crypto service provide
        /// </summary>
        private RNGCryptoServiceProvider _rng;

        /// <summary>
        /// Creates new instance of <see cref="PasswordHashService"/>
        /// </summary>
        public PasswordHashService()
        {
            this._rng = new RNGCryptoServiceProvider();
        }

        /// <summary>
        /// Does hash operation for password
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>hash of password</returns>
        public string HashPassword(string password)
        {
            // initializing salt
            var salt = new byte[16];

            // getting random bytes for salt
            this._rng.GetBytes(salt);

            // creating password-based key derivation function 2
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            // getting bytes
            var hash = pbkdf2.GetBytes(20);

            // hashing
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // returning the hash of password
            return Convert.ToBase64String(hashBytes);
        }
    }
}