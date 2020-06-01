using EnsafMarket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.Areas.Admin.ViewModels
{
    public class ContactsViewModel
    {
        public IEnumerable<Contact> Contacts { get; set; }
    }
}