using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace EnsafMarket.Core.Models
{
    public class ContactFeedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Feedback { get; set; }

        [Required]
        public int ContactId { get; set; }

        [JsonIgnore]
        public Contact Contact { get; set; }

        public int? FromUserId { get; set; }

        [JsonIgnore]
        public User FromUser { get; set; }

        public int? ToUserId { get; set; }

        [JsonIgnore]
        public User ToUser { get; set; }
    }
}
