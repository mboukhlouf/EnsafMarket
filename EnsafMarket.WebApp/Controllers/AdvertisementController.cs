using EnsafMarket.Core.Models;
using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.Core.Models.Api.Responses;
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

        public async Task<ActionResult> Offers(string q = null, int page = 1)
        {
            int count = 10;
            await GetUserAsync();
            var response = await GetAdvertisementsAsync(AdvertisementType.Offer, q, page);

            ViewData["Title"] = "Offres";
            return View("List", new ListAdvertisementsModelView
            {
                User = user,
                CurrentPage = page,
                TotalCount = response.Count,
                MaxPage = (response.Count / count) + 1,
                Advertisements = response.Advertisements
            });
        }

        public async Task<ActionResult> Requests(string q = null, int page = 1)
        {
            int count = 10;
            await GetUserAsync();
            var response = await GetAdvertisementsAsync(AdvertisementType.Request, q, page);

            ViewData["Title"] = "Demandes";
            return View("List", new ListAdvertisementsModelView
            {
                User = user,
                CurrentPage = page,
                TotalCount = response.Count,
                MaxPage = (response.Count / count) + 1,
                Advertisements = response.Advertisements
            });
        }

        private async Task<GetAdvertisementsResponse> GetAdvertisementsAsync(AdvertisementType type, string q, int page)
        {
            int count = 10;
            int start = (page - 1) * count;
            var request = new GetAdvertisementsRequest
            {
                Search = q,
                Type = type,
                Count = count,
                Start = start
            };
            var response = await emClient.GetAdvertisementsAsync(request);
            return response;
        }
    }
}