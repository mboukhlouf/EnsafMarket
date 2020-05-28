using EnsafMarket.Core.Models.Api.Requests.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Requests
{
    public class PostAdvertisementRequest : BaseRequest
    {
        [Required]
        public string Title { get; set; }

        public AdvertisementType Type { get; set; }

        public AdvertisementContentType ContentType { get; set; }

        public string Description { get; set; }
    }
}
