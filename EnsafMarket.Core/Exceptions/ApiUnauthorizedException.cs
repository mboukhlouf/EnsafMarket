using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.Core.Exceptions
{
    public class ApiUnauthorizedException : ApiException
    {
        public ApiUnauthorizedException() : base("Unauthorized")
        {
        }
    }
}
