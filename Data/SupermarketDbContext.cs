using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Data
{
    public class SupermarketDbContext : DbContext
    {

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options)
        : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                 .ToTable("Product");

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(250)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Category>()
                .ToTable("Category");

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Product>()
                .Property(c => c.Name)
                .HasMaxLength(250)
                .IsRequired();
        }

        

    }
}