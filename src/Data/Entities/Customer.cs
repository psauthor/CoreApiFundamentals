using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Data.Entities
{
    public class Customer
    {
        public int    Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IncludedItems { get; set; }
        public string LogoUrl { get; set; }
        public int CustomerPrice { get; set; }
        public string LunchPrice { get; set; }
        public string OpenHours { get; set; }
    }
}
