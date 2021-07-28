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
            modelBuilder.Entity<Birthday>().HasData(new Birthday
            {
                Note = "Не забыть поздравить хоть в этот раз",
                Name = "Петров Петр Петрович",
                DateOfBirth = new DateTime(28, 07, 1990),
                Relationship = "Брат"
            },
            new Birthday
            {
                Note = "Новая заметка",
                Name = "Петя Александров",
                DateOfBirth = new DateTime(05, 05, 1985),
                Relationship = "Брат"
            },
            new Birthday
            {
                Note = "Заметка 3",
                Name = "Алексей Иванов Александрович",
                DateOfBirth = new DateTime(01, 15, 1955),
                Relationship = "Друг"
            });
        }
    }
}
