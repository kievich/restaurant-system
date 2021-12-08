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
        Draft,
        Active,
        Сompleted,
        Сanceled
    }

    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public int DishCount { get; set; }
        [NotMapped]
        public int TotalPrice { get; set; }

    }
}
