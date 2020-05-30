using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace EnsafMarket.Core.Models
{
    public class ContactMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public int? ContactId { get; set; }

        [JsonIgnore]
        public Contact Contact { get; set; }

        public ContactMessage()
        {
            DateTime = DateTime.Now;
        }
    }
}