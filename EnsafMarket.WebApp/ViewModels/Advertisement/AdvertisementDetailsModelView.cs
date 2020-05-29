using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.ViewModels
{
    public class AdvertisementDetailsModelView : BaseViewModel
    {
        public Core.Models.Advertisement Advertisement { get; set; }

        public IEnumerable<Core.Models.Advertisement> SimilarAdvertisements { get; set; }

        [Display(Name = "Message de contact")]
        public string ContactMessage { get; set; }

        public string ErrorMessage { get; set; }
    }
}