using AlexonTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AlexonTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
           new Product
           {
               Id = 1,
               Name = "Product A",
               Description = "Description for Product A",
               Price = 15.99m,
               CategoryIds = new List<int> { 1, 2 },
               CreatedDate = new DateTime(2023, 1, 10),
               UpdateDate = new DateTime(2023, 1, 15)
           },
        new Product
        {
            Id = 2,
            Name = "Product B",
            Description = "Description for Product B",
            Price = 29.99m,
            CategoryIds = new List<int> { 2, 3 },
            CreatedDate = new DateTime(2023, 2, 5),
            UpdateDate = new DateTime(2023, 2, 12)
        },
        new Product
        {
            Id = 3,
            Name = "Product C",
            Description = "Description for Product C",
            Price = 9.99m,
            CategoryIds = new List<int> { 1, 3 },
            CreatedDate = new DateTime(2023, 3, 20),
            UpdateDate = new DateTime(2023, 3, 25)
        });
        }
    }
}
