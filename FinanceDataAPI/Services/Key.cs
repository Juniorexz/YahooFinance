using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDataAPI.Services
{
    public class Key
    {
        public static string Secret = "123as4d56asd45ads465a4s5d6";

        public static byte[] Get256BitKey()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(Secret);
                return sha256.ComputeHash(keyBytes);
            }
        }
    }
}
