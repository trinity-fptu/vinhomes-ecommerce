using Domain.Models;
using Infrastructure.FluentAPIs.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.FluentAPIs;

public class CategoryConfiguration : GenericConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.Property(category => category.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(category => category.Description)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.HasMany(category => category.Products)
            .WithOne(product => product.Category)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Product_Category");
    }
}



