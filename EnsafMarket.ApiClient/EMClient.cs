using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.Core.Models.Api.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnsafMarket.ApiClient
{
    public class EMClient : IDisposable
    {
        private readonly ApiProcessor apiProcessor;

        private bool disposed = false;

        public string Token
        {
            get => apiProcessor.Token;
            set => apiProcessor.Token = value;
        }

        public EMClient()
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

        ~EMClient()
        {
            Dispose(false);
        }

        #region User

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var endpoint = Endpoints.Users.Register();
            var response = await apiProcessor.ProcessRequestAsync<RegisterResponse>(endpoint, request);
            return response;
        }

        public Task<LoginResponse> LoginAsync(string email, string password)
        {
            return LoginAsync(new LoginRequest
            {
                Email = email,
                Password = password
            });
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var endpoint = Endpoints.Users.Login();
            var response = await apiProcessor.ProcessRequestAsync<LoginResponse>(endpoint, request);
            if (response.Result)
            {
                Token = response.Token;
            }
            return response;
        }

        public async Task<GetUserResponse> GetUserAsync()
        {
            var endpoint = Endpoints.Users.GetUser();
            var response = await apiProcessor.ProcessRequestAsync<GetUserResponse>(endpoint);
            return response;
        }

        public async Task<GetUserResponse> GetUserAsync(int id)
        {
            var endpoint = Endpoints.Users.GetUser(id);
            var response = await apiProcessor.ProcessRequestAsync<GetUserResponse>(endpoint);
            return response;
        }

        public async Task<GetUserAdvertisementsResponse> GetUserAdvertisementsAsync(int id)
        {
            var endpoint = Endpoints.Users.GetUserAdvertisements(id);
            var response = await apiProcessor.ProcessRequestAsync<GetUserAdvertisementsResponse>(endpoint);
            return response;
        }

        public async Task<GetUserContactsResponse> GetUserContactsAsync(int id)
        {
            var endpoint = Endpoints.Users.GetUserContacts(id);
            var response = await apiProcessor.ProcessRequestAsync<GetUserContactsResponse>(endpoint);
            return response;
        }

        #endregion

        #region Advertisement

        public async Task<GetAdvertisementsResponse> GetAdvertisementsAsync(GetAdvertisementsRequest request)
        {
            var endpoint = Endpoints.Advertisements.GetAdvertisements();
            var response = await apiProcessor.ProcessRequestAsync<GetAdvertisementsResponse>(endpoint, request);
            return response;
        }

        public async Task<GetAdvertisementResponse> GetAdvertisementAsync(int id)
        {
            var endpoint = Endpoints.Advertisements.GetAdvertisement(id);
            var response = await apiProcessor.ProcessRequestAsync<GetAdvertisementResponse>(endpoint);
            return response;
        }

        public async Task<PostAdvertisementResponse> PostAdvertisementAsync(PostAdvertisementRequest request)
        {
            var endpoint = Endpoints.Advertisements.PostAdvertisement();
            var response = await apiProcessor.ProcessRequestAsync<PostAdvertisementResponse>(endpoint, request);
            return response;
        }

        #endregion

        #region Contact
        public async Task<GetContactResponse> GetContactAsync(int id)
        {
            var endpoint = Endpoints.Contacts.GetContact(id);
            var response = await apiProcessor.ProcessRequestAsync<GetContactResponse>(endpoint);
            return response;
        }

        public async Task<PostContactFeedbackResponse> PostContactAsync(PostContactFeedbackRequest request)
        {
            var endpoint = Endpoints.Contacts.PostContact();
            var response = await apiProcessor.ProcessRequestAsync<PostContactFeedbackResponse>(endpoint, request);
            return response;
        }

        public async Task<GetContactMessagesResponse> GetContactMessagesAsync(int id)
        {
            var endpoint = Endpoints.Contacts.GetContactMessages(id);
            var response = await apiProcessor.ProcessRequestAsync<GetContactMessagesResponse>(endpoint);
            return response;
        }

        public async Task<PostContactMessageResponse> PostContactAsync(int id, PostContactMessageRequest request)
        {
            var endpoint = Endpoints.Contacts.PostContactMessage(id);
            var response = await apiProcessor.ProcessRequestAsync<PostContactMessageResponse>(endpoint, request);
            return response;
        }

        public async Task<GetContactFeedbacksResponse> GetContactFeedbacksAsync(int id)
        {
            var endpoint = Endpoints.Contacts.GetContactFeedbacks(id);
            var response = await apiProcessor.ProcessRequestAsync<GetContactFeedbacksResponse>(endpoint);
            return response;
        }

        public async Task<PostContactFeedbackResponse> PostContactFeedbackAsync(int id, PostContactFeedbackRequest request)
        {
            var endpoint = Endpoints.Contacts.PostContactFeedback(id);
            var response = await apiProcessor.ProcessRequestAsync<PostContactFeedbackResponse>(endpoint, request);
            return response;
        }

        public async Task<PutContactFeedbackResponse> PutContactFeedbackAsync(int id, PutContactFeedbackRequest request)
        {
            var endpoint = Endpoints.Contacts.PutContactFeedback(id);
            var response = await apiProcessor.ProcessRequestAsync<PutContactFeedbackResponse>(endpoint, request);
            return response;
        }

        #endregion
    }
}
