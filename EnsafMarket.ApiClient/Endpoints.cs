using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.ApiClient
{
    internal static class Endpoints
    {
        private static string ApiBaseUrl { get; } = "https://localhost:44338";

        private static string ApiPrefix { get; } = $"{ApiBaseUrl}/api";

        public static class User
        {
            public static EndpointData Login => new EndpointData(new Uri($"{ApiPrefix}/User/Login"), EndpointSecurityType.None);
            public static EndpointData Register => new EndpointData(new Uri($"{ApiPrefix}/User/Register"), EndpointSecurityType.None);
        }

        public static class Advertisement
        {
            public static EndpointData Create => new EndpointData(new Uri($"{ApiPrefix}/Advertisement/Create"), EndpointSecurityType.ApiKey);
        }
    }
}
