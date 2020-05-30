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
using Microsoft.IdentityModel.Tokens;

namespace EnsafMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private IConfiguration config;
        private readonly EnsafMarketDbContext context;

        public StatsController(IConfiguration config, EnsafMarketDbContext context)
        {
            this.config = config;
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<StatsResponse>> Get()
        {
            var response = new StatsResponse()
            {
                Result = true,
                StudentsCount = context.User.Where(user => user.Status == UserStatus.Student || user.Status == UserStatus.Laureat)
                                            .Count(),
                ProfessorsCount = context.User.Where(user => user.Status == UserStatus.Professor)
                                            .Count(),
                AdvertisementsCount = context.Advertisement.Count()
            };
            return response;
        }
    }
}
