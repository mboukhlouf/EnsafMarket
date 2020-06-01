using EnsafMarket.Core.Models.Api.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Responses
{
    public class GetUsersResponse : BaseResponse
    {
        public IEnumerable<User> Users { get; set; }
    }
}
