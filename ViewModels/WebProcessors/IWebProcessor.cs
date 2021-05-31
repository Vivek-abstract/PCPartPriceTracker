using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.WebProcessors
{
    public interface IWebProcessor
    {
        string Url { get; set; }
        Product GetProductStockAndPrice(Product product);
    }
}
