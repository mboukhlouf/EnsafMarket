using EnsafMarket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public User ProfileUser { get; set; }
        
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}