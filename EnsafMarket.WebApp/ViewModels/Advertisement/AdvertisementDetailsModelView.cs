using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.ViewModels.Advertisement
{
    public class AdvertisementDetailsModelView : BaseViewModel
    {
        public Core.Models.Advertisement Advertisement { get; set; }
    }
}