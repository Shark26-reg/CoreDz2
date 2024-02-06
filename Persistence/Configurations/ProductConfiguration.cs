using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(250);

        builder.Property(x => x.Description).HasMaxLength(250);

        builder.Property(x => x.Price).IsRequired();

        builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId);       
    }
}
