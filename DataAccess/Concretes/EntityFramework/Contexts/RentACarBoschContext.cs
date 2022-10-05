using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework.Contexts
{
    public class RentACarBoschContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
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

            // Seed
            Brand[] brandSeeds = { new() { Id = 1, Name = "Audi" } };
            modelBuilder.Entity<Brand>().HasData(brandSeeds);
        }
    }
}

// Migration oluşturmak ve kullanmak için:
// Add-Migration AddBrands
// Update-Database