using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPartPriceTracker.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool InStock { get; set; }

        public double TargetPrice { get; set; }

        public double Price { get; set; }

    }
}
