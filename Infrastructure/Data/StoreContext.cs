
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
        }
    }
}