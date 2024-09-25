using BoxingGearReview.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BoxingGearReview.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        // Customize tables 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User 1:N Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            // Equipment 1:N Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Equipment)
                .WithMany(e => e.Reviews)
                .HasForeignKey(r => r.EquipmentId);

            {
                modelBuilder.Entity<Review>()
                    .Property(r => r.Rating)
                    .HasPrecision(4, 2);  // Exemplo: precisão de 4 dígitos com 2 decimais
            }

            // Configurar a precisão e escala do decimal Price
            modelBuilder.Entity<Equipment>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)"); // Ajuste a precisão e a escala conforme necessário

            //  Category 1:N Equipment
            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Equipments)
                .HasForeignKey(e => e.CategoryId);

            // Brand 1:N Equipment
            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Brand)
                .WithMany(b => b.Equipments)
                .HasForeignKey(e => e.BrandId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

