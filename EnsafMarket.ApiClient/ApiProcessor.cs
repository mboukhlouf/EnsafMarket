using EnsafMarket.Core.Exceptions;
using EnsafMarket.Core.Models.Api.Requests.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EnsafMarket.ApiClient
{
    internal class ApiProcessor : IDisposable
    {
        private HttpClientHandler httpClientHandler;
        private HttpClient httpClient;

        private bool disposed = false;

        private static readonly string AuthorizationHeader = "Authorization";

        private static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public String Token { get; set; }

        public ApiProcessor()
        {
            httpClientHandler = new HttpClientHandler()
            {
                UseCookies = false,
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    httpClientHandler.Dispose();
                    httpClient.Dispose();
                }

                disposed = true;
            }
        }

        ~ApiProcessor()
        {
            Dispose(false);
        }

        private async Task<HttpResponseMessage> CreateRequestAsync(EndpointData endpoint, HttpMethod method, BaseRequest requestObject)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();

            if (method == HttpMethod.Get)
            {
                UriBuilder uriBuilder = new UriBuilder(endpoint.Uri);
                uriBuilder.Query = GenerateQueryString(requestObject);
                requestMessage.RequestUri = uriBuilder.Uri;
            }
            else
            {
                requestMessage.RequestUri = endpoint.Uri;
                string content = JsonConvert.SerializeObject(requestObject, SerializerSettings);
                requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            requestMessage.Method = method;

            if (endpoint.SecurityType == EndpointSecurityType.ApiKey)
            {
                requestMessage.Headers.Add(AuthorizationHeader, $"Bearer {Token}");
            }

            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendAsync(requestMessage);
                requestMessage.Dispose();
                return response;
            }
            catch (Exception)
            {
                requestMessage.Dispose();
                throw;
            }
        }

        public async Task<T> ProcessGetRequestAsync<T>(EndpointData endpoint, BaseRequest requestObject) where T : class
        {
            HttpResponseMessage message = await CreateRequestAsync(endpoint, HttpMethod.Get, requestObject);
            return await HandleJsonResponseAsync<T>(message);
        }

        public async Task<T> ProcessDeleteRequestAsync<T>(EndpointData endpoint, BaseRequest requestObject) where T : class
        {
            HttpResponseMessage message = await CreateRequestAsync(endpoint, HttpMethod.Delete, requestObject);
            return await HandleJsonResponseAsync<T>(message);
        }

        public async Task<T> ProcessPostRequestAsync<T>(EndpointData endpoint, BaseRequest requestObject) where T : class
        {
            HttpResponseMessage message = await CreateRequestAsync(endpoint, HttpMethod.Post, requestObject);
            return await HandleJsonResponseAsync<T>(message);
        }

        public async Task<T> ProcessPutRequestAsync<T>(EndpointData endpoint, BaseRequest requestObject) where T : class
        {
            HttpResponseMessage message = await CreateRequestAsync(endpoint, HttpMethod.Put, requestObject);
            return await HandleJsonResponseAsync<T>(message);
        }

        private static async Task<T> HandleJsonResponseAsync<T>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();

                T obj;
                try
                {
                    obj = JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception e)
                {
                    responseMessage.Dispose();
                    throw new ApiException("Unable to deserialize response message.", e);
                }

                if (obj == null)
                {
                    responseMessage.Dispose();
                    throw new ApiException("Unable to deserialize response message.");
                }

                return obj;
            }

            var body = await responseMessage.Content.ReadAsStringAsync();
            responseMessage.Dispose();
            throw CreateAivaException(responseMessage.StatusCode, body);

        }

        private static ApiException CreateAivaException(HttpStatusCode statusCode, string message)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return new ApiUnauthorizedException();
            }
            return new ApiException(message);
        }

        private static String GenerateQueryString(BaseRequest requestObject)
        {
            if (requestObject == null)
            {
                throw new ArgumentNullException(nameof(requestObject));
            }

            JObject obj = JObject.FromObject(requestObject, JsonSerializer.Create(SerializerSettings));
            return String.Join("&", obj.Children()
                .Cast<JProperty>()
                .Where(j => j.Value != null)
                .Select(j => j.Name + "=" + WebUtility.UrlEncode(j.Value.ToString())));
        }
    }
}
