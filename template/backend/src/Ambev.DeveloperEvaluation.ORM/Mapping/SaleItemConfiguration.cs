﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItem");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("uuid");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.ProductId).IsRequired().HasColumnType("uuid");
            builder.Property(p => p.Discount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasOne<Sale>().WithMany(s => s.SaleItens).HasForeignKey(p => p.SaleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
