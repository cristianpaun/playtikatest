using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Mapping
{
    public class OrderDetailMap : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasOne(od => od.Order).WithMany(c => c.OrderDetails).HasForeignKey(od => od.OrderId);
            builder.HasOne(od => od.Product).WithMany().HasForeignKey(od => od.ProductId);

            builder.Property(od => od.OrderId).IsRequired();
            builder.Property(od => od.ProductId).IsRequired();
            builder.Property(od => od.Quantity).IsRequired();
        }
    }
}
