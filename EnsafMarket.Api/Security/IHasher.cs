using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsafMarket.Api.Security
{
    interface IHasher
    {
        String HashPassword(String password);

        bool CheckPassword(String text, String hash);
    }
}
