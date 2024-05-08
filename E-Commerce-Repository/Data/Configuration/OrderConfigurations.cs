using E_Commerce_Core.Enities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Data.Configuration
{
    internal class OrderConfigurations :IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           
            builder.HasMany(order=>order.OrderItems)
                .WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,5)");

            builder.OwnsOne(order => order.ShippingAdress, o => o.WithOwner());


        }
    }
}
