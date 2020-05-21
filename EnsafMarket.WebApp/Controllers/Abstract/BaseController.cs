using EnsafMarket.ApiClient;
using EnsafMarket.Core.Exceptions;
using EnsafMarket.Core.Models;
using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.WebApp.ViewModels.Authentication;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EnsafMarket.WebApp.Controllers.Abstract
{
    public abstract class BaseController : Controller
    {
        protected EMClient emClient;
        protected User user;

        public BaseController()
        {
            emClient = new EMClient();
        }

        protected async Task<User> GetUserAsync()
        {
            string token = Request?.Cookies["token"]?.Value;
            emClient.Token = token;
            user = null;
            try
            {
                var response = await emClient.GetUserAsync();
                user = response.User;
            }
            catch (ApiUnauthorizedException)
            {
            }
            return user;
        }

        protected override void Dispose(bool disposing)
        {
            emClient.Dispose();
            base.Dispose(disposing);
        }
    }
}