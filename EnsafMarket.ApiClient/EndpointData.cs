using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.ApiClient
{
    internal class EndpointData
    {
        public Uri Uri { get; set; }
        public EndpointSecurityType SecurityType { get; set; }

        public EndpointData(Uri uri, EndpointSecurityType securityType)
        {
            Uri = uri;
            SecurityType = securityType;
        }

        public override string ToString()
        {
            return Uri.ToString();
        }
    }
}
