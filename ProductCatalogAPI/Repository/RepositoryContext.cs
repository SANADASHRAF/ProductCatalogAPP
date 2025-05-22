using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<ApplicationUser>
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
          {
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        
        public void SeedInitialData()
        {
            if (!Categories.Any())
            {
                Categories.AddRange(
                    new Category {  Name = "Books" },
                    new Category {  Name = "Clothing" },
                    new Category {  Name = "Games" }
                );
            }
            SaveChanges();
        }

        }
}
