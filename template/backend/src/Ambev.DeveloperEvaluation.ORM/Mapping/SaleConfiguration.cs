using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid");
            builder.Property(s => s.Date).IsRequired();
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.BranchId).IsRequired().HasMaxLength(50);
            builder.Property(s => s.TotalSaleAmount).HasColumnType("decimal(18,2)");
            builder.Property(s => s.IsCanceled).IsRequired();
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.HasMany(s => s.SaleItens).WithOne().HasForeignKey(p => p.SaleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
