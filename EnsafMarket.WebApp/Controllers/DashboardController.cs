using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using EnsafMarket.Core.Models.Api.Requests;
using EnsafMarket.WebApp.Controllers.Abstract;
using EnsafMarket.WebApp.ViewModels;

namespace EnsafMarket.WebApp.Controllers
{
    public class DashboardController : BaseController
    {
        [Route("Dashboard")]
        public async Task<ActionResult> Index()
        {

            return View(new DashboardViewModel());
        }
    }
}