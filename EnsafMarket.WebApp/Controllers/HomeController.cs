﻿using EnsafMarket.WebApp.Controllers.Abstract;
using EnsafMarket.WebApp.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnsafMarket.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            await GetUserAsync();
            return View(new HomeViewModel
            {
                User = user
            });
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            await GetUserAsync();
            return View(new HomeViewModel
            {
                User = user
            });
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Your contact page.";
            await GetUserAsync();
            return View(new HomeViewModel
            {
                User = user
            });
        }
    }
}