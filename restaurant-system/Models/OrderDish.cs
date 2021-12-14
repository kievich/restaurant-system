using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant_system.Models
{
    public class OrderDish
    {
        public int Id { get; set; }
        [ForeignKey("DishId_FK")]
        public Dish Dish { get; set; }
        [ForeignKey("OrderId_FK")]
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}
