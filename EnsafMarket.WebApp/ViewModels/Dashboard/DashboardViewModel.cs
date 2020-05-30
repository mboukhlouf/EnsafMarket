using EnsafMarket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }
    }
}