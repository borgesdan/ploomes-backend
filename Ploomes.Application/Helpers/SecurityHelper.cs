using System.Security.Cryptography;
using System.Text;

namespace Ploomes.Application.Helpers
{
    public class SecurityHelper
    {
        /// <summary>Obtém um hash de uma chave informada.</summary>        
        public static string CreateHash(string key)
        {
            var sourceBytes = Encoding.UTF8.GetBytes(key);
            var hashBytes = SHA1.HashData(sourceBytes);
            var hash = BitConverter
                .ToString(hashBytes)
                .ToLower()
                .Replace("-", string.Empty);           

            return hash;
        }
    }
}