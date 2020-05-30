using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EnsafMarket.Core.Models
{
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public AdvertisementType Type { get; set; }

        public AdvertisementContentType ContentType { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public User Owner { get; set; }

        [JsonIgnore]
        public ICollection<Contact> Contacts { get; set; }

        public Advertisement()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
