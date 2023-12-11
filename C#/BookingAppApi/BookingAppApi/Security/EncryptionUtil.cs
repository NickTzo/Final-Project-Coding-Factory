using Microsoft.CodeAnalysis.Scripting;


namespace BookingAppApi.Security
{
    public class EncryptionUtil
    {
        /// <summary>
        /// A method to hash password
        /// </summary>
        /// <param name="password">The password that the user want to hash</param>
        /// <returns>The hashed password</returns>
        public static string Encrypt(string password)
        {
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return encryptedPassword;
        }

        /// <summary>
        /// A method to encure that the password in the data base and the user password is the same
        /// </summary>
        /// <param name="plainText">The psasword that the user type</param>
        /// <param name="cipherText">The hashed password that is saved in the database</param>
        /// <returns>If its the same returns true otherwise false</returns>
        public static bool IsValidPassword(string plainText, string cipherText)
        {
            var isValid = BCrypt.Net.BCrypt.Verify(plainText, cipherText);
            return isValid;
        }
    }
}
