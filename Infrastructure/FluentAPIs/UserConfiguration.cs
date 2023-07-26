using Domain.Models;
using Infrastructure.FluentAPIs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.FluentAPIs;

public class UserConfiguration : GenericConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.PhotoUrl).IsRequired(false);

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Order_User");
        
        builder.HasMany(u => u.Ratings)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Rating_User");

        builder.HasOne(u => u.Store)
            .WithOne(s => s.StoreOwner)
            .HasForeignKey<Store>(s => s.StoreOwnerId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_User_Store")
            .IsRequired(false);
    }
}

