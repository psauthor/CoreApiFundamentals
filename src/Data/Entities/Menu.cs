using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DayOfWeek { get; set; }
        public int CustomerId { get; set; }
    }
}
