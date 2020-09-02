
using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        /*The below method is one of the methods that's called when a migration is being created.
        Here we override the method and apply our own configurations. 
        This is how it works
        - Override onModelCreating which is part of DBContext class
        - pass modelBuilder which is just a model of the context being created (storecontext)
        - Applies configuration from all IEntityTypeConfiguration<TEntity> from the assembly 
        - The GetExecutingAssembly part is just a way of saying that the configurations are in the same assembly 
        that is executing the DbContext class (the Infrastructure.dll assembly). */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Applies configuration from all IEntityTypeConfiguration<TEntity> 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            /*
            Here we're checking to see if any of our database entities are of decimal type. We want to convert from decimal to
            double as we cannot sort entities if they are of decimal type. (For example the price)
            So we find the property which has a decimal type and then apply a conversion to double. 
            It is better to do it here as we would be applying this change to this migration which gets run as soon as the app starts.
            You could use a double type for price inside Product class instead of Decimal however it seems double is a little more
            tricky to work for currencies. Not sure why. Need to research this or maybe it is something you find out as you gain more
            experience. 
            */
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    
                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
        }
    }
}