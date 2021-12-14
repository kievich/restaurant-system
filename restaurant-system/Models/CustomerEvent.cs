using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_system.Models
{
    public class CustomerEvent
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Event Event { get; set; }
    }
}
