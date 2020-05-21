using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.ViewModels.Authentication
{
    public class LoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Champs requis")]
        [Display(Name = "Adresse email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Champs requis")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}