using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_system.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [ForeignKey("TableId_FK")]
        public Table Table { get; set; }

        public float Hours
        {
            get
            {
                var tolalHours = (float)(End - Start).TotalHours;
                return tolalHours > 0.01f ? tolalHours : 0;
            }
        }
    }
}
