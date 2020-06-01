using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using EnsafMarket.WebApp.Areas.Admin.Controllers.Abstract;

namespace EnsafMarket.WebApp.Areas.Admin.Controllers
{
    public class LoginController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(dynamic obj)
        {
            return RedirectToAction("Index", "Users");
        }
    }
}