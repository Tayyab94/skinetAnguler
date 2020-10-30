// using API.Entities;
using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext :DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }

            public DbSet<ProductType> ProductTypes { get; set; }
                    public DbSet<ProdcutBrand> ProductBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            // Convertion Decimel Model Entity Property type to Decimal...
            if(Database.ProviderName=="Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entytyType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties=entytyType.ClrType.GetProperties().Where(p=>p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entytyType.Name).Property(property.Name)
                        .HasConversion<double>();
                    }
                }
            }
        }
        
    }
}