using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.Core.Models.Api.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnsafMarket.ApiClient
{
    public class ApiClient : IDisposable
    {
        private readonly ApiProcessor apiProcessor;

        private bool disposed = false;

        public string Token
        {
            get => apiProcessor.Token;
            set => apiProcessor.Token = value;
        }

        public string RefreshToken { get; private set; }

        public ApiClient()
        {
            apiProcessor = new ApiProcessor();
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
                    apiProcessor.Dispose();
                }

                disposed = true;
            }
        }

        ~ApiClient()
        {
            Dispose(false);
        }

        #region User
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var response = await apiProcessor.ProcessPostRequestAsync<RegisterResponse>(Endpoints.User.Register, request);
            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = await apiProcessor.ProcessPostRequestAsync<LoginResponse>(Endpoints.User.Login, request);
            if (response.Result)
            {
                Token = response.Token;
            }
            return response;
        }

        #endregion

        #region Advertisement
        public async Task<GetAdvertisementsResponse> GetAdvertisementsAsync(GetAdvertisementsRequest request)
        {
            var response = await apiProcessor.ProcessPostRequestAsync<GetAdvertisementsResponse>(Endpoints.Advertisement.Get, request);
            return response;
        }

        public async Task<CreateAdvertisementResponse> CreateAdvertisementAsync(CreateAdvertisementRequest request)
        {
            var response = await apiProcessor.ProcessPostRequestAsync<CreateAdvertisementResponse>(Endpoints.Advertisement.Create, request);
            return response;
        }
        #endregion
    }
}
