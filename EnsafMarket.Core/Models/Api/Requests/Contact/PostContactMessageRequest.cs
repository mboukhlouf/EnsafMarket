using EnsafMarket.Core.Models.Api.Requests.Abstract;
using EnsafMarket.Core.Models.Api.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Requests
{
    public class PostContactMessageRequest : BaseRequest
    {
        public string Content { get; set; }
    }
}
