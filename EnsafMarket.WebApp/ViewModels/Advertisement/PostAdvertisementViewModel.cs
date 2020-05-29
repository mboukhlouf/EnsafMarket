using EnsafMarket.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.ViewModels
{
    public class PostAdvertisementViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Titre")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Type d'annonce")]
        public AdvertisementType Type { get; set; }

        [Required]
        [Display(Name = "Contenu de l'annonce")]
        public AdvertisementContentType ContentType { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}