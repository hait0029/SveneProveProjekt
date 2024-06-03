﻿
namespace SvenePrøveProjekt.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Login> Login { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductList> ProductOrderList { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<City> City { get; set; }


    }
}
