using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsafMarket.Api.Security
{
    public class BCryptHasher : IHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool CheckPassword(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
    }
}
