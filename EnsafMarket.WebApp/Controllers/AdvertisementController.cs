using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.WebApp.Controllers.Abstract;
using EnsafMarket.WebApp.ViewModels.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnsafMarket.WebApp.Controllers
{
    public class AdvertisementController : BaseController
    {
        // GET: Advertisement/Post
        public async Task<ActionResult> Post()
        {
            await GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            return View(new PostAdvertisementViewModel
            { 
                User = user
            });
        }

        // POST: Advertisement/Post
        [HttpPost]
        public async Task<ActionResult> Post(PostAdvertisementViewModel model)
        {
            await GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var request = new CreateAdvertisementRequest
            {
                Title = model.Title,
                Type = model.Type,
                ContentType = model.ContentType,
                Description = model.Description
            };

            var response = await emClient.CreateAdvertisementAsync(request);

            if (response.Result)
            {
                // Redirect to Ad details
                return RedirectToAction("Details", new { id = response.Advertisement.Id });
            }
            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var response = await emClient.GetAdvertisementsAsync((int)id);
            if(response.Count != 1)
            {
                return HttpNotFound();
            }
            var advertisement = response.Advertisements.First();
            var advertisementUserResponse = await emClient.GetUserAsync(advertisement.OwnerId);
            advertisement.Owner = advertisementUserResponse.User;
            await GetUserAsync();
            return View(new AdvertisementDetailsModelView
            {
                User = user,
                Advertisement = advertisement
            });
        }
    }
}