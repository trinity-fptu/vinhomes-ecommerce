using Domain.Models;
using Infrastructure.FluentAPIs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.FluentAPIs;

public class OrderConfiguration : GenericConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        
        builder.Property(order => order.OrderDate)
            .IsRequired();
        builder.Property(order => order.OrderNumber)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(order => order.Total)
            .IsRequired();

        builder.HasMany(order => order.OrderDetails)
            .WithOne(orderDetail => orderDetail.Order)
            .HasForeignKey(orderDetail => orderDetail.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OrderDetail_Order");

        builder.HasOne(order => order.User)
            .WithMany(customer => customer.Orders)
            .HasForeignKey(order => order.CustomerId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Order_Customer");


        builder.HasOne(order => order.Store)
            .WithMany(store => store.Orders)
            .HasForeignKey(order => order.StoreId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Order_Store");
    }
}
