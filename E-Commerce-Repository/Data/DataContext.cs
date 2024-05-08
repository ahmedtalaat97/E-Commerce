using E_Commerce_Core.Enities;
using E_Commerce_Core.Enities.OrderEntities;
using E_Commerce_Repository.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Data
{
    public class DataContext : DbContext
    {




        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public DbSet<Product> Products { get; set; }


        public DbSet<ProductType> ProductTypes { get; set; }


        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConfig());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);


        }
        


    }
}
