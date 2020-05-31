using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.WebApp.Controllers.Abstract;
using EnsafMarket.WebApp.ViewModels;

namespace EnsafMarket.WebApp.Controllers
{
    public class DashboardController : BaseController
    {
        [Route("Dashboard")]
        public async Task<ActionResult> Index()
        {
            await GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var advertisementsResponse = await emClient.GetUserAdvertisementsAsync(user.Id);
            var advertisements = advertisementsResponse.Advertisements;

            var contactsResponse = await emClient.GetUserContactsAsync(user.Id);
            var contacts = contactsResponse.Contacts;

            return View(new DashboardViewModel
            {
                Advertisements = advertisements,
                Contacts = contacts,
                User = user
            });
        }
    }
}