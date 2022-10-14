using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework.Contexts
{
    public class RentACarBoschContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Model> Models { get; set; }
        // Ef'in varsayılan davranışı Brand nesnesinin tablo karşılığını "Brands" şeklinde tanımasınıdır.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Windows Auth için "...;Integrated Security=True"
            // Sql Auth için:
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=RentACarBosch;Integrated Security=False;User Id=sa;Password=Passw0rd");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(b =>
            {
                b.ToTable("Brands").HasKey(k => k.Id);
                b.Property(b => b.Id).HasColumnName("Id");
                b.Property(b => b.Name).HasColumnName("Name").IsRequired();
            });

            modelBuilder.Entity<Model>(m =>
            {
                m.HasKey(m => m.Id);
                m.Property(m => m.Id).HasColumnName("Id");
                m.Property(m => m.BrandId).HasColumnName("BrandId").IsRequired();
                m.Property(m => m.Name).HasColumnName("Name").IsRequired();
                m.HasOne(m => m.Brand);
            });

            // Seed
            Brand[] brandSeeds = { new() { Id = 1, Name = "Audi" } };
            modelBuilder.Entity<Brand>().HasData(brandSeeds);
        }
    }
}

// Migration oluşturmak ve kullanmak için:
// Add-Migration AddBrands
// Update-Database