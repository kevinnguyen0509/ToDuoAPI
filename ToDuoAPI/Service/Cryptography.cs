using System.Security.Cryptography;
using System.Text;

namespace ToDuoAPI.Service

{
    public class Cryptography
    {
        /// <summary>
        /// Takes in a password string, then hashs and salts the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>returns a string password that is hashed and salted</returns>
        public string CreateHashAndSaltPassword(string password, string salt)
        {
            string hashedAndSaltPassword = HashPassword($"{password}{salt}");
            return hashedAndSaltPassword;
        }

        /// <summary>
        /// This will be used to verify a password
        /// </summary>
        /// <param name="serverPassword">The encrypted password in the database</param>
        /// <param name="password">what the user typed in the form</param>
        /// <param name="salt">salt comes from the user's salt column</param>
        /// <returns>bool value</returns>
        public bool VerifyHashedPassword(string serverPassword, string password, string salt)
        {
            bool isSuccessful = serverPassword == HashPassword($"{password}{salt}") ? true : false;
            return isSuccessful;
        }

        public string createSalt()
        {
            return DateTime.Now.ToString();
        }

        /// <summary>
        /// This is a private method used to Has a password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>A hashed password as a string</returns>
        private string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPassword = hash.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashedPassword); ;
        }


    }
}
