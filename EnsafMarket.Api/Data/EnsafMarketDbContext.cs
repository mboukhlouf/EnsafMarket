using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnsafMarket.Core.Models;

namespace EnsafMarket.Api.Models
{
    public class EnsafMarketDbContext : DbContext
    {
        public EnsafMarketDbContext(DbContextOptions<EnsafMarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<EnsafMarket.Core.Models.User> User { get; set; }

        public DbSet<EnsafMarket.Core.Models.Advertisement> Adertisement { get; set; }

        public DbSet<EnsafMarket.Core.Models.Contact> Contact { get; set; }

        public DbSet<EnsafMarket.Core.Models.ContactMessage> ContactMessage { get; set; }

        public DbSet<EnsafMarket.Core.Models.ContactFeedback> ContactFeedback { get; set; }
    }
}
