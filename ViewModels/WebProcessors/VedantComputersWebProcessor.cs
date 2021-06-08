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

        public Task<Product> GetProductStockAndPrice(Product product)
        {
            return Task.Run(() =>
            {
                try
                {
                    HtmlWeb web = new HtmlWeb();

                    HtmlDocument htmlDoc = web.Load(Url);

                    var node = htmlDoc.QuerySelector(".product-price");

                    if (node != null && double.TryParse(node.InnerText.Trim(new char[] { '₹' }), out double price))
                    {
                        product.Price = price;
                    }
                    else
                    {
                        product.Price = -1;
                    }

                    node = htmlDoc.DocumentNode.QuerySelector(".product-info");
                    
                    if (node != null && !node.HasClass("out-of-stock"))
                    {
                        product.InStock = true;
                    }
                    else
                    {
                        product.InStock = false;
                    }
                } 
                catch(Exception ex)
                {
                    product.InStock = false;
                    product.Price = -1;
                }

                return product;
            });
        }
    }
}
