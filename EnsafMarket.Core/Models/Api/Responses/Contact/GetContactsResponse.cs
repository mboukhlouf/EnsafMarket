using EnsafMarket.Core.Models.Api.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Responses
{
    public class GetContactsResponse : BaseResponse
    {
        public IEnumerable<Contact> Contacts { get; set; }

        public int Count { get; set; }
    }
}
