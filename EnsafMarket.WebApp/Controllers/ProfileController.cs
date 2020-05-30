using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using EnsafMarket.WebApp.Controllers.Abstract;
using EnsafMarket.WebApp.ViewModels;

namespace EnsafMarket.WebApp.Controllers
{
    [RoutePrefix("Profile")]
    public class ProfileController : BaseController
    {
        [Route]
        public async Task<ActionResult> Index()
        {
            await GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            return RedirectToAction("Details", new { id = user.Id });
        }

        [Route("{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            await GetUserAsync();

            var userResponse = await emClient.GetUserAsync(id);
            if(!userResponse.Result)
            {
                return HttpNotFound();
            }

            var profileUser = userResponse.User;
            var advertisementsResponse = await emClient.GetUserAdvertisementsAsync(profileUser.Id);
            var advertisements = advertisementsResponse.Advertisements;

            foreach(var ad in advertisements)
            {
                ad.Owner = profileUser;
            }

            return View(new ProfileViewModel
            {
                User = user,
                ProfileUser = profileUser,
                Advertisements = advertisements
            });
        }
    }
}