using EnsafMarket.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Adresse email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirmez le mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Prenom")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nom")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "CNE")]
        [DataType(DataType.Text)]
        public string Cne { get; set; }

        [Required]
        [Display(Name = "Statut")]
        public UserStatus Status { get; set; }

        [Required]
        [Display(Name = "Filière")]
        [DataType(DataType.Text)]
        public string Major { get; set; }

        [Required]
        [Display(Name = "Année")]
        [DataType(DataType.Text)]
        public string Year { get; set; }

        [Required]
        [Display(Name = "Loisirs")]
        [DataType(DataType.Text)]
        public string Hobbies { get; set; }

        public string ErrorMessage { get; set; }
    }
}