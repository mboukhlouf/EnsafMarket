using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public IEnumerable<ContactMessage> Messages { get; set; }

        [JsonIgnore]
        public IEnumerable<ContactFeedback> Feedbacks { get; set; }
        
        [JsonIgnore]
        public ContactFeedback OwnerFeedback
        {
            get
            {
                if (Feedbacks == null || Advertisement == null)
                    return null;

                return Feedbacks.Where(feedback => feedback.FromUserId == Advertisement.OwnerId).FirstOrDefault();
            }
        }

        [JsonIgnore]
        public ContactFeedback UserFeedback
        {
            get
            {
                if (Feedbacks == null || UserId == null)
                    return null;

                return Feedbacks.Where(feedback => feedback.FromUserId == UserId).FirstOrDefault();
            }
        }

        public Contact()
        {
            CreatedAt = DateTime.Now;
        }
    }
}