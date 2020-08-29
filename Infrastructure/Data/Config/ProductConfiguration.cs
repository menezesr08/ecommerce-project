using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /* Here we can configure the entity 'Product'. This allows us to change some of the 
        properties when creating a migration. 
        */
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            // We are setting the data type of the price entity to decimal and to 2 decimal places.
            builder.Property(p => p.Price).HasColumnType("decimal(18, 2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            // Here we associate a relationship between a product and product brand. 
            // A product brand can have many products
            builder.HasOne(b => b.productBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(t => t.productType).WithMany().HasForeignKey(p => p.ProductTypeId);
        }
    }
}