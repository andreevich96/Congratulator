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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dt = new DateTime();
            var f = dt.AddDays(28).AddMonths(7).AddYears(1990);
            var f1 = dt.AddDays(05).AddMonths(05).AddYears(1985);
            var f2 = dt.AddDays(1).AddMonths(02).AddYears(1955);

            modelBuilder.Entity<Birthday>().HasData(new Birthday
            {
                Id = 100,
                Note = "Не забыть поздравить хоть в этот раз",
                Name = "Петров Петр Петрович",
                DateOfBirth = f,
                Relationship = "Брат"
            },
            
            new Birthday
            {
                Id = 101,
                Note = "Новая заметка",
                Name = "Петя Александров",
                DateOfBirth = f1,
                Relationship = "Брат"
            },

            new Birthday
            {
                Id = 102,
                Note = "Заметка 3",
                Name = "Алексей Иванов Александрович",
                DateOfBirth = f2,
                Relationship = "Друг"
            });
        }
    }
}
