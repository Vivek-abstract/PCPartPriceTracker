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
    public class MDComputersWebProcessor : IWebProcessor
    {
        public string Url { get; set; }

        public MDComputersWebProcessor(string url)
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

                    var htmlDoc = web.Load(Url);

                    var node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[3]/div/div/div[2]/div/div[2]/div[3]/span/span");

                    if (node != null && double.TryParse(node.InnerText.Trim(new char[] { '₹' }), out double price))
                    {
                        product.Price = price;
                    }
                    else
                    {
                        product.Price = -1;
                    }

                    node = htmlDoc.DocumentNode.QuerySelector(".stock");

                    if (node != null && !node.InnerText.Contains("Out Of Stock"))
                    {
                        product.InStock = true;
                    }
                    else
                    {
                        product.InStock = false;
                    }
                }
                catch (Exception ex)
                {
                    product.Price = -1;
                    product.InStock = false;
                }

                return product;
            });
        }
    }
}
