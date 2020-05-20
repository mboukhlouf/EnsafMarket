using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnsafMarket.WebApp.Controllers
{
    public class AdvertisementController : Controller
    {
        // GET: Advertisement
        public ActionResult Listad()
        {
            ViewBag.Message = "Liste des Annonces";
            return View();
        }
        public ActionResult Addad()
        {
            ViewBag.Message = "Ajouter votre annonce";
            return View();
        }

    }
}