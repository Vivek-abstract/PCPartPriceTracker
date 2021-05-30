using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPartPriceTracker.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool InStock { get; set; }

        public double TargetPrice { get; set; }

        public double Price { get; set; }

        // Price color based on whether price is less than target price
        [NotMapped]
        public string PriceColor
        {
            get
            {
                return Price <= TargetPrice ? "#50fa7b" : "Red" ;
            }
        }

    }
}
