using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Responses.Abstract
{
    public abstract class BaseResponse
    {
        public bool Result { get; set; }

        public string Message { get; set; }
    }
}
