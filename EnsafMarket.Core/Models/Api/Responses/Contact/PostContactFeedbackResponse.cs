﻿using EnsafMarket.Core.Models.Api.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Responses
{
    public class PostContactFeedbackResponse : BaseResponse
    {
        public ContactFeedback ContactFeedback { get; set; }
    }
}
