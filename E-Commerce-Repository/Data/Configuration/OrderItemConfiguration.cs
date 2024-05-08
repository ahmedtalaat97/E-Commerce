using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Core.Enities.OrderEntities;

namespace E_Commerce_Repository.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.OwnsOne(o => o.OrderItemProduct, o => o.WithOwner());

            builder.Property(o => o.Price).HasColumnType("decimal(18,5)");



        }
    }
}
