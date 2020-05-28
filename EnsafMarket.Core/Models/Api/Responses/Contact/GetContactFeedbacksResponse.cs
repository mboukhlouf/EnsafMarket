using EnsafMarket.Core.Models.Api.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Responses
{
    public class GetContactFeedbacksResponse : BaseResponse
    {
        public ContactFeedback OwnerFeedback { get; set; }
        public ContactFeedback UserFeedback { get; set; }
    }
}