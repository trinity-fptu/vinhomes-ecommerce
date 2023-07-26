using Domain.Models;
using Infrastructure.FluentAPIs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.FluentAPIs;

public class InventoryConfiguration : GenericConfiguration<Inventory>
{
    public override void Configure(EntityTypeBuilder<Inventory> builder)
    {
        base.Configure(builder);

        builder.Property(inventory => inventory.Quantity)
            .IsRequired();
        builder.Property(inventory => inventory.LastUpdated)
            .IsRequired();

        builder.HasOne(inventory => inventory.Product)
            .WithOne(product => product.Inventory)
            .HasForeignKey<Inventory>(inventory => inventory.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Inventory_Product");
    }
}


