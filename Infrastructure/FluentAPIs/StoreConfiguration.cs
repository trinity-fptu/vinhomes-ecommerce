using Domain.Models;
using Infrastructure.FluentAPIs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.FluentAPIs;

public class StoreConfiguration : GenericConfiguration<Store>
{
    public override void Configure(EntityTypeBuilder<Store> builder)
    {
        base.Configure(builder);

        builder.HasMany(s => s.Products)
            .WithOne(p => p.Store)
            .HasForeignKey(p => p.StoreId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Product_Store");

        builder.HasOne(s => s.StoreOwner)
            .WithOne(u => u.Store)
            .HasForeignKey<User>(u => u.StoreId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_User_Store")
            .IsRequired(false);

        builder.HasMany(store => store.Orders)
            .WithOne(order => order.Store)
            .HasForeignKey(order => order.StoreId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Order_Store");
    }
}
