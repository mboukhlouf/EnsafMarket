using EnsafMarket.Core.Models.Api.Requests.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Requests
{
    public class GetAdvertisementsRequest : BaseRequest
    {

        public string Q { get; set; }

        public AdvertisementType? Type { get; set; }

        public AdvertisementContentType? ContentType { get; set; }

        public int? UserId { get; set; }

        public int? Offset { get; set; }

        public int? Limit { get; set; }
    }
}
