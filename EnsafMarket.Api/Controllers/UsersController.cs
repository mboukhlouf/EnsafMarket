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
    public class UsersController : ControllerBase
    {
        private IConfiguration config;
        private readonly EnsafMarketDbContext context;

        public UsersController(IConfiguration config, EnsafMarketDbContext context)
        {
            this.config = config;
            this.context = context;
        }

        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUsersResponse>> GetUsersAsync()
        {
            // NEED TO BE ONLY ACCESSIBLE BY ADMIN
            var users = await context.User.ToListAsync();

            return new GetUsersResponse
            {
                Result = true,
                Users = users
            };
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
        {
            RegisterResponse response = new RegisterResponse();
            if(!ModelState.IsValid)
            {
                response.Result = false;
                response.Message = "Model not valid";
                return BadRequest(response);
            }

            // Check if there is already an account with the same email
            bool emailExists = context.User.Where(u => u.Email == request.Email.ToLower()).Any();
            if(emailExists)
            {
                response.Result = false;
                response.Message = "Email already in use by a different account.";
                return Ok(response);
            }

            IHasher hasher = new BCryptHasher();
            User user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email.ToLower(),
                Password = hasher.HashPassword(request.Password),
                Address = request.Address,
                Cne = request.Cne,
                DateOfBirth = request.DateOfBirth,
                Hobbies = request.Hobbies,
                Major = request.Major,
                Status = request.Status,
                Year = request.Year
            };

            context.User.Add(user);
            await context.SaveChangesAsync();
            response.Result = true;
            response.Message = "Account registered successfully.";
            return CreatedAtAction("Register", response);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public ActionResult<LoginResponse> Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            var user = ValidateUser(request.Email, request.Password);

            if (user == null)
            {
                response.Result = false;
                response.Message = "Authentication failed.";
                return Unauthorized(response);
            }

            var token = GenerateJwt(user);
            response.Result = true;
            response.Token = token;
            return Ok(response);
        }

        [HttpGet("{id?}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserResponse>> GetAsync(int? id = null)
        {
            var response = new GetUserResponse();
            User user;
            if (id == null)
            {
                UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
                if (userClaims == null)
                {
                    response.Result = false;
                    response.Message = "Unauthorized";
                    return Unauthorized(response);
                }

                user = await context.User.FindAsync(userClaims.Id);
            }
            else
            {
                user = await context.User.FindAsync(id);
            }

            if(user != null)
            {
                response.Result = true;
                response.User = user;
                return response;
            }
            else
            {
                response.Result = false;
                response.Message = "User not found.";
                return NotFound(response);
            }
        }

        [HttpGet("{id}/Advertisements")]
        [AllowAnonymous]
        public async Task<ActionResult<GetUserAdvertisementsResponse>> GetAdvertisementsAsync(int id)
        {
            var ads = context.Advertisement.AsQueryable();
            ads = ads.Where(ad => ad.OwnerId == id);
            return new GetUserAdvertisementsResponse
            {
                Result = true,
                Advertisements = await ads.ToListAsync()
            };
        }

        [HttpGet("{id}/Contacts")]
        [Authorize]
        public async Task<ActionResult<GetUserContactsResponse>> GetContactsAsync(int id)
        {
            UserClaims userClaims = UserClaims.FromClaimsPrincipal(User);
            if(userClaims.Id != id)
            {
                return Unauthorized(new GetUserContactsResponse
                {
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            var contacts = context.Contact.AsQueryable();
            contacts = contacts.Where(contact => contact.Advertisement.OwnerId == id || contact.UserId == id)
                .Include(nameof(Contact.User))
                .Include(nameof(Contact.Advertisement));
            return new GetUserContactsResponse
            {
                Result = true,
                Contacts = await contacts.ToListAsync()
            };
        }

        private string GenerateJwt(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            UserClaims userClaims = new UserClaims(user.Id, user.Email);

            var token = new JwtSecurityToken(issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims.Claims.Values,
                expires: DateTime.Now.AddDays(365),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User ValidateUser(string email, string password)
        {
            User user = context.User.FirstOrDefault(
                u => u.Email.Equals(email.ToLower()));

            if (user != null)
            {
                IHasher hasher = new BCryptHasher();
                bool passwordValid = hasher.CheckPassword(password, user.Password);

                if (passwordValid)
                    return user;
            }
            return null;
        }
    }
}