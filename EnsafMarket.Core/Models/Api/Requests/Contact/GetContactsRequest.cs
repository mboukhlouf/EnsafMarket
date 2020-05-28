using EnsafMarket.Core.Models.Api.Requests.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Requests
{
    public class GetContactsRequest : BaseRequest
    {
        public int? Id { get; set; }

        public int? AdvertisementId { get; set; }

        public int? UserId { get; set; }

        public int? Start { get; set; }

        public int? Count { get; set; }
    }
}
