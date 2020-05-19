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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly EnsafMarketDbContext context;

        public AdvertisementController(EnsafMarketDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<GetAdvertisementsResponse>> Get(GetAdvertisementsRequest request)
        {
            var ads = context.Advertisement.AsQueryable();
            // Search
            if (request.Search != null)
            {
                string search = request.Search.Trim().ToLower();
                ads = ads.Where(ad => ad.Title.ToLower().Contains(search));
            }  

            // Order by CreationDate
            ads = ads.OrderByDescending(ad => ad.CreatedAt);

            int totalCount = ads.Count();
            int start = 0;
            int count = 10;
            if (request.Start != null)
                start = (int)request.Start;

            if (request.Count != null)
                count = (int)request.Count;

            ads = ads.Skip(start)
                .Take(count);

            return new GetAdvertisementsResponse
            {
                Result = true,
                Count = totalCount,
                Advertisements = await ads.ToListAsync()
            };
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateAdvertisementResponse>> Create(CreateAdvertisementRequest request)
        {
            CreateAdvertisementResponse response = new CreateAdvertisementResponse();
            if (!ModelState.IsValid)
            {
                response.Result = false;
                response.Message = "Model not valid";
                return BadRequest(response);
            }

            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if(userClaims == null)
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
            return CreatedAtAction("Create", response);
        }
    }
}