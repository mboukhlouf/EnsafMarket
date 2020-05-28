using EnsafMarket.Core.Models.Api.Requests.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Requests
{
    public class GetUserRequest : BaseRequest
    {
        public int? Id { get; set; }
    }
}
