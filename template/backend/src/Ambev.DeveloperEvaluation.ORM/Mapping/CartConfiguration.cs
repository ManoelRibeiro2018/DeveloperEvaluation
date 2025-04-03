using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnType("uuid");
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.UserId).IsRequired().HasColumnType("uuid");

            builder.OwnsMany<CartItem>(e => e.Products, rt =>
            {
                rt.Property(e => e.ProductId).HasColumnName("Count").HasColumnType("uuid");
                rt.Property(e => e.Quantity).HasColumnName("Rate").HasColumnType("int");
            });
        }
    }
}