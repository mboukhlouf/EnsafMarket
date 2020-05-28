using EnsafMarket.Core.Models.Api.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Responses
{
    public class PostContactResponse : BaseResponse
    {
        public Contact Contact { get; set; }
    }
}
