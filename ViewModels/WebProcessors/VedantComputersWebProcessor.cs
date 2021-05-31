using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.WebProcessors
{
    public class VedantComputersWebProcessor : IWebProcessor
    {
        public string Url { get; set; }

        public VedantComputersWebProcessor(string url)
        {
            Url = url;
        }

        public Product GetProductStockAndPrice(Product product)
        {
            HtmlWeb web = new HtmlWeb();

            HtmlDocument htmlDoc = web.Load(Url);

            var node = htmlDoc.QuerySelector(".product-price");

            if (double.TryParse(node.InnerText.Trim(new char[] { '₹' }), out double price))
            {
                product.Price = price;
            }
            else
            {
                throw new HtmlWebException("Price not present");
            }

            node = htmlDoc.DocumentNode.QuerySelector(".product-info");

            if (node.HasClass("out-of-stock"))
            {
                product.InStock = false;
            }
            else
            {
                product.InStock = true;
            }

            return product;
        }
    }
}
