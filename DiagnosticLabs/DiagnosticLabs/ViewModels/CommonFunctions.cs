using System;
using System.Security.Cryptography;
using System.Text;

namespace DiagnosticLabs.ViewModels
{
    public class CommonFunctions
    {
        public string HashPassword(string password)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);

                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }
    }
}
