using EnsafMarket.Core.Models.Api.Requests.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Requests
{
    public class GetAdvertisementsRequest : BaseRequest
    {
        public string Search { get; set; }

        public AdvertisementType? Type { get; set; }

        public AdvertisementContentType? ContentType { get; set; }

        public int? Start { get; set; }

        public int? Count { get; set; }
    }
}
