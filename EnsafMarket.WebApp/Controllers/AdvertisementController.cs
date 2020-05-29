using EnsafMarket.Core.Models;
using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.Core.Models.Api.Responses;
using EnsafMarket.WebApp.Controllers.Abstract;
using EnsafMarket.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnsafMarket.WebApp.Controllers
{
    [RoutePrefix("Advertisement")]
    public class AdvertisementController : BaseController
    {
        // GET: Advertisement
        [Route]
        public async Task<ActionResult> Index()
        {
            return RedirectToAction("Offers");
        }

        // GET: Advertisement/Post
        [Route("Post")]
        [HttpGet]
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
        [Route("Post")]
        [HttpPost]
        public async Task<ActionResult> Post(PostAdvertisementViewModel model)
        {
            await GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var request = new PostAdvertisementRequest
            {
                Title = model.Title,
                Type = model.Type,
                ContentType = model.ContentType,
                Description = model.Description
            };

            var response = await emClient.PostAdvertisementAsync(request);

            if (response.Result)
            {
                // Redirect to Ad details
                return RedirectToAction("Details", new { id = response.Advertisement.Id });
            }
            return View(model);
        }

        [Route("{id:int}", Name = "Advertisement Details")]
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var response = await emClient.GetAdvertisementAsync((int)id);
            if(response.Advertisement == null)
            {
                return HttpNotFound();
            }
            var advertisement = response.Advertisement;
            var advertisementUserResponse = await emClient.GetUserAsync(advertisement.OwnerId);
            advertisement.Owner = advertisementUserResponse.User;
            await GetUserAsync();

            var similarAdsResponse = await emClient.GetSimilarAdvertisementsAsync((int)id, 3);
            var similarAds = similarAdsResponse.Advertisements;

            return View(new AdvertisementDetailsModelView
            {
                User = user,
                Advertisement = advertisement,
                SimilarAdvertisements = similarAds
            });
        }

        [Route("{id:int}")]
        [HttpPost]
        public async Task<ActionResult> Details(int id, AdvertisementDetailsModelView model)
        {
            var response = await emClient.GetAdvertisementAsync((int)id);
            if (response.Advertisement == null)
            {
                return HttpNotFound();
            }
            var advertisement = response.Advertisement;
            var advertisementUserResponse = await emClient.GetUserAsync(advertisement.OwnerId);
            advertisement.Owner = advertisementUserResponse.User;

            await GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var similarAdsResponse = await emClient.GetSimilarAdvertisementsAsync((int)id, 3);
            var similarAds = similarAdsResponse.Advertisements;

            if (user.Id == advertisement.OwnerId)
            {
                string errorMessage = "Tu ne peux pas créer un contact avec toi même.";
                return View(new AdvertisementDetailsModelView
                {
                    User = user,
                    Advertisement = advertisement,
                    SimilarAdvertisements = similarAds,
                    ContactMessage = model.ContactMessage,
                    ErrorMessage = errorMessage
                });
            }

            var postContactResponse = await emClient.PostContactAsync(new PostContactRequest
            {
                AdvertisementId = (int)advertisement.Id
            });

            if(!postContactResponse.Result)
            {
                string errorMessage = postContactResponse.Message;
                return View(new AdvertisementDetailsModelView
                {
                    User = user,
                    Advertisement = advertisement,
                    SimilarAdvertisements = similarAds,
                    ContactMessage = model.ContactMessage,
                    ErrorMessage = errorMessage
                });
            }

            try
            {
                var postContactMessageResponse = await emClient.PostContactMessageAsync(postContactResponse.Contact.Id, new PostContactMessageRequest
                {
                    Content = model.ContactMessage
                });
            }
            catch(Exception)
            {
            }

            return RedirectToAction("Details", "Contact", new { id = postContactResponse.Contact.Id });
        }

        [Route("~/Offers")]
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

        [Route("~/Requests")]
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
                Q = q,
                Type = type,
                Limit = count,
                Offset = start
            };
            var response = await emClient.GetAdvertisementsAsync(request);
            return response;
        }
    }
}