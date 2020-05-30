using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EnsafMarket.Api.Authorization;
using EnsafMarket.Api.Models;
using EnsafMarket.Api.Security;
using EnsafMarket.Core.Models;
using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.Core.Models.Api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EnsafMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementsController : ControllerBase
    {
        private readonly EnsafMarketDbContext context;

        public AdvertisementsController(EnsafMarketDbContext context)
        {
            this.context = context;
        }

        // GET: api/Advertisement
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<GetAdvertisementsResponse>> GetAdvertisements([FromQuery]GetAdvertisementsRequest request)
        {
            var ads = context.Advertisement.AsQueryable();

            // User Id
            if (request.UserId != null)
            {
                var userId = (int)request.UserId;
                ads = ads.Where(ad => ad.OwnerId == userId);
            }

            // Type
            if (request.Type != null)
            {
                var type = (AdvertisementType)request.Type;
                ads = ads.Where(ad => ad.Type == type);
            }

            // Content type
            if (request.ContentType != null)
            {
                var contentType = (AdvertisementContentType)request.ContentType;
                ads = ads.Where(ad => ad.ContentType == contentType);
            }

            // Search
            if (request.Q != null)
            {
                string search = request.Q.Trim().ToLower();
                ads = ads.Where(ad => ad.Title.ToLower().Contains(search));
            }

            // Order by CreationDate
            ads = ads.OrderByDescending(ad => ad.CreatedAt);

            int totalCount = ads.Count();

            if (request.Offset != null)
                ads = ads.Skip((int)request.Offset);

            if (request.Limit != null)
                ads = ads.Take((int)request.Limit);

            ads = ads.Include(nameof(Advertisement.Owner));
            var response = new GetAdvertisementsResponse
            {
                Result = true,
                Count = totalCount,
                Advertisements = await ads.ToListAsync()
            };
            return response;
        }

        // GET: api/Advertisement/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetAdvertisementResponse>> GetAdvertisement(int id)
        {
            var advertisement = await context.Advertisement.FindAsync(id);

            if (advertisement == null)
            {
                return NotFound(new GetAdvertisementResponse
                {
                    Result = false,
                    Message = "Not found"
                });
            }
            context.Entry(advertisement).Reference(nameof(advertisement.Owner)).Load();
            return new GetAdvertisementResponse
            {
                Result = true,
                Message = "",
                Advertisement = advertisement
            };
        }

        // POST: api/Advertisement
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostAdvertisementResponse>> PostAdvertisement(PostAdvertisementRequest request)
        {
            PostAdvertisementResponse response = new PostAdvertisementResponse();
            if (!ModelState.IsValid)
            {
                response.Result = false;
                response.Message = "Model not valid";
                return BadRequest(response);
            }

            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if (userClaims == null)
            {
                response.Result = false;
                response.Message = "Unauthorized";
                return Unauthorized(response);
            }

            Advertisement advertisement = new Advertisement
            {
                Title = request.Title,
                Description = request.Description,
                ContentType = request.ContentType,
                Type = request.Type,
                OwnerId = userClaims.Id
            };

            context.Advertisement.Add(advertisement);
            await context.SaveChangesAsync();

            response.Result = true;
            response.Message = "Advertisement created.";
            response.Advertisement = advertisement;
            return CreatedAtAction("GetAdvertisement", new { id = advertisement.Id },response);
        }

        // GET: api/Advertisement/5/Similar
        [HttpGet("{id}/Similar")]
        [AllowAnonymous]
        public async Task<ActionResult<GetSimilarAdvertisementsResponse>> GetSimilarAdvertisements(int id, int count = 3)
        {
            var currentAdvertisement = await context.Advertisement.FindAsync(id);
            if (currentAdvertisement == null)
            {
                return NotFound(new GetAdvertisementResponse
                {
                    Result = false,
                    Message = "Not found"
                });
            }

            var similarAdvertisements = new List<Advertisement>();
            var possibleAds = context.Advertisement.Where(ad => ad.ContentType == currentAdvertisement.ContentType && ad.Type == currentAdvertisement.Type)
                                                    .Include(nameof(Advertisement.Owner));

            if (count > possibleAds.Count())
            {
                count = possibleAds.Count();
            }

            Random rand = new Random(id);
            while (similarAdvertisements.Count < count)
            {
                int toSkip = rand.Next(0, possibleAds.Count());
                var ad = possibleAds.Skip(toSkip).Take(1).First();
                if (!similarAdvertisements.Contains(ad))
                {
                    similarAdvertisements.Add(ad);
                }
            }

            return new GetSimilarAdvertisementsResponse
            {
                Result = true,
                Advertisements = similarAdvertisements
            };
        }
    }
}