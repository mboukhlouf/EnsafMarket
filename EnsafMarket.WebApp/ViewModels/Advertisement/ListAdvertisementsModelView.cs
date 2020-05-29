using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.ViewModels
{
    public class ListAdvertisementsModelView : BaseViewModel
    {
        public int CurrentPage { get; set; } 

        public int MaxPage { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<Core.Models.Advertisement> Advertisements { get; set; }
    }
}