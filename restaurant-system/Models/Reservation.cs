using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_system.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TableId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
