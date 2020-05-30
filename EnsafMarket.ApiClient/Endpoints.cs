using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EnsafMarket.ApiClient
{
    internal static class Endpoints
    {
        private static string ApiBaseUrl { get; } = "https://localhost:44338";

        private static string ApiPrefix { get; } = $"{ApiBaseUrl}/api";

        public static class Users
        {
            private static string Controller { get; } = "Users";

            public static EndpointData Login()
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/Login"), HttpMethod.Post, EndpointSecurityType.None);
            }

            public static EndpointData Register()
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/Register"), HttpMethod.Post, EndpointSecurityType.None);
            }

            public static EndpointData GetUser()
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}"), HttpMethod.Get, EndpointSecurityType.ApiKey);
            }

            public static EndpointData GetUser(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}"), HttpMethod.Get, EndpointSecurityType.None);
            }

            public static EndpointData GetUserAdvertisements(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}/Advertisements"), HttpMethod.Get, EndpointSecurityType.None);
            }

            public static EndpointData GetUserContacts(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}/Contacts"), HttpMethod.Get, EndpointSecurityType.ApiKey);
            }
        }

        public static class Advertisements
        {
            private static string Controller { get; } = "Advertisements";

            public static EndpointData GetAdvertisements()
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}"), HttpMethod.Get, EndpointSecurityType.None);
            }

            public static EndpointData GetAdvertisement(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}"), HttpMethod.Get, EndpointSecurityType.None);
            }

            public static EndpointData PostAdvertisement()
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}"), HttpMethod.Post, EndpointSecurityType.ApiKey);
            }

            public static EndpointData GetSimilarAdvertisements(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}/Similar"), HttpMethod.Get, EndpointSecurityType.None);
            }
        }

        public static class Contacts
        {
            private static string Controller { get; } = "Contacts";

            public static EndpointData GetContact(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}"), HttpMethod.Get, EndpointSecurityType.ApiKey);
            }

            public static EndpointData PostContact()
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}"), HttpMethod.Post, EndpointSecurityType.ApiKey);
            }

            public static EndpointData GetContactMessages(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}/Messages"), HttpMethod.Get, EndpointSecurityType.ApiKey);
            }

            public static EndpointData PostContactMessage(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}/Messages"), HttpMethod.Post, EndpointSecurityType.ApiKey);
            }

            public static EndpointData GetContactFeedbacks(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}/Feedbacks"), HttpMethod.Get, EndpointSecurityType.ApiKey);
            }

            public static EndpointData PostContactFeedback(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}/Feedbacks"), HttpMethod.Post, EndpointSecurityType.ApiKey);
            }

            public static EndpointData PutContactFeedback(int id)
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}/{id}/Feedbacks"), HttpMethod.Put, EndpointSecurityType.ApiKey);
            }
        }

        public static class Stats
        {
            private static string Controller { get; } = "Stats";

            public static EndpointData GetStats()
            {
                return new EndpointData(new Uri($"{ApiPrefix}/{Controller}"), HttpMethod.Get, EndpointSecurityType.None);
            }
        }
    }
}
