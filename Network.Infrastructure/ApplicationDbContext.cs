using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network.Core.Shared.Entities;

namespace Network.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Birthday> Birthdays { get; set; }
        //public DbSet<Article> Articles => Set<Article>();      

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
      
    }
}
