using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ViewModels.WebProcessors
{
    public class PrimeAbgbWebProcessor : IWebProcessor
    {
        public string Url { get; set; }
        public PrimeAbgbWebProcessor(string url)
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

                    web.PreRequest += delegate (HttpWebRequest request)
                    {
                        request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
                        request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                        request.CookieContainer = new System.Net.CookieContainer();
                        return true;
                    };
                    var htmlDoc = web.Load(Url);

                    // When there are two prices - an old one and a new one
                    var node = htmlDoc.DocumentNode.QuerySelector(".price ins .woocommerce-Price-amount bdi");
                    double price;

                    if (node != null && double.TryParse(node.InnerText.Split(";")[1], out price))
                    {
                        product.Price = price;
                    }
                    else
                    {
                        // When there is only one price
                        node = htmlDoc.DocumentNode.QuerySelector(".price .woocommerce-Price-amount bdi");
                        if (node != null && double.TryParse(node.InnerText.Split(";")[1], out price))
                        {
                            product.Price = price;
                        }
                        else
                        {
                            product.Price = -1;
                        }
                    }

                    node = htmlDoc.DocumentNode.QuerySelector(".single_add_to_cart_button");

                    if (node != null)
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
