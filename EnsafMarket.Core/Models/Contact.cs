using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnsafMarket.Core.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public int AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<ContactMessage> Messages { get; set; }

        public int? OwnerFeedbackId { get; set; }

        public ContactFeedback OwnerFeedback { get; set; }

        public int? UserFeedbackId { get; set; }

        public ContactFeedback UserFeedback { get; set; }

        public Contact()
        {
            CreatedAt = DateTime.Now;
        }
    }
}