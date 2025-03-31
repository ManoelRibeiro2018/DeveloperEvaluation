using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnType("uuid");
            builder.Property(e => e.Title).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.Category).IsRequired();
            builder.Property(e => e.Image).IsRequired();
            builder.OwnsOne<Rating>(e => e.Rating, rt =>
            {
                rt.Property(e => e.Count).HasColumnName("Count").HasColumnType("int");
                rt.Property(e => e.Rate).HasColumnName("Rate").HasColumnType("decimal(10,2)");
            });
        }
    }
}
