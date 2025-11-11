using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product>Products { get; set; }

        public DbSet<Invoice>Invoices { get; set; } 
        public DbSet<InvoiceItem> invoiceItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Invoice>()
               .HasOne(i => i.User)
               .WithMany()
               .HasForeignKey(i => i.UserId)
               .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<InvoiceItem>()
                .HasOne(it => it.Invoice)
                .WithMany()
                .HasForeignKey(it => it.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<InvoiceItem>()
              .HasOne(it => it.Product)
              .WithMany()
              .HasForeignKey(it => it.ProductId)
              .OnDelete(DeleteBehavior.Restrict);

        }


        public class YourDbContext : IDesignTimeDbContextFactory<AppDbContext>
        {
            private readonly IConfiguration config;

            public YourDbContext() { }
            public YourDbContext(IConfiguration _config)
            {
                config = _config;
            }

            public AppDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer("Server=LAPTOP-IC28PODO\\SQLEXPRESS;Database=UserProductManagement;Trusted_Connection=True;TrustServerCertificate=True;");

                return new AppDbContext(optionsBuilder.Options);
            }
        }
    }
}
