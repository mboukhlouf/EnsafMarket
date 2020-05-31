using EnsafMarket.ApiClient;
using EnsafMarket.Core.Exceptions;
using EnsafMarket.Core.Models;
using EnsafMarket.Core.Models.Api.Requests;
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
    public class AuthenticationController : BaseController
    {
        // GET: Login
        [Route("Login")]
        public async Task<ActionResult> Login()
        {
            await GetUserAsync();
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }

        // POST: Login
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var response = await emClient.LoginAsync(model.Email, model.Password);

            if (response.Result)
            {
                Response.Cookies.Add(new HttpCookie("token", emClient.Token));
                return RedirectToAction("Offers", "Advertisements");
            }
            model.Password = null;
            model.ErrorMessage = "Email/Mot de passe invalide";
            return View(model);
        }

        // GET: Register
        [Route("Register")]
        public async Task<ActionResult> Register()
        {
            await GetUserAsync();
            if (user != null)
            {
                RedirectToAction("Index", "Home");
            }
            return View(new RegisterViewModel());
        }

        // POST: Register
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var request = new RegisterRequest
            {
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Cne = model.Cne,
                DateOfBirth = model.DateOfBirth,
                Hobbies = model.Hobbies,
                Major = model.Major,
                Status = model.Status,
                Year = model.Year
            };

            var response = await emClient.RegisterAsync(request);

            if (response.Result)
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [Route("Logout")]
        // GET: Logout
        public ActionResult Logout()
        {
            var cookie = new HttpCookie("token");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Login", "Authentication");
        }
    }
}