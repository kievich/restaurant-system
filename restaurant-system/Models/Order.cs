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
        Unassigned = 1,
        Assigned = 2,
        Сompleted = 3,
        Сanceled = 4
    }

    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("OrderId_FK")]
        public Table Table { get; set; }
        public string WaiterName { get; set; }
        public string CookName { get; set; }
        public string Name { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }

    }
}
