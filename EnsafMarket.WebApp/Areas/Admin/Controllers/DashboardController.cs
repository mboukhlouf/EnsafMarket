using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using EnsafMarket.WebApp.Areas.Admin.Controllers.Abstract;

namespace EnsafMarket.WebApp.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}