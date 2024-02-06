using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks");
        builder.HasKey(x => new { x.ProductId, x.StoreId });

        builder.Property(x => x.Quantity).IsRequired();

        builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);
        builder.HasOne<Store>().WithMany().HasForeignKey(x => x.StoreId);
    }
}