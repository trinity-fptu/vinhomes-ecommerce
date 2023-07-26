using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.FluentAPIs.Base;

public abstract class GenericConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(typeof(T).Name);

        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty != null)
        {
            builder.HasKey("Id");
            builder
                .Property("Id")
                .HasDefaultValueSql("NEWID()");
        }
        else
        {
            throw new Exception($"Type {typeof(T).Name} does not contain a property named 'Id'");
        }

        // Configure properties of type string
        foreach (var property in typeof(T).GetProperties()
                     .Where(p => p.PropertyType == typeof(string)))
        {
            builder.Property(property.Name).HasMaxLength(100);
        }

    }
}


