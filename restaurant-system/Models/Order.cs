using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant_system.Models
{
    public enum OrderStatus
    {
        Draft = 0,
        Active = 1,
        Сompleted = 2,
        Сanceled = 3
    }

    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public int DishCount { get; set; }
        [NotMapped]
        public int TotalPrice { get; set; }

    }
}
