using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EnsafMarket.Core.Models
{
    public class ContactFeedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Feedback { get; set; }

        [Required]
        public int AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; }

        [Required]
        public int FromUserId { get; set; }

        public User FromUser { get; set; }

        [Required]
        public int ToUserId { get; set; }

        public User ToUser { get; set; }
    }
}
