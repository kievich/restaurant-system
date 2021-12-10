using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restaurant_system.Models;

namespace restaurant_system
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> OrderDishes { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
             : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
