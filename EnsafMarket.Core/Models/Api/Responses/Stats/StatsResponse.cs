using EnsafMarket.Core.Models.Api.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnsafMarket.Core.Models.Api.Responses
{
    public class StatsResponse : BaseResponse
    {
        public int StudentsCount { get; set; }

        public int ProfessorsCount { get; set; }

        public int AdvertisementsCount { get; set; }
    }
}
