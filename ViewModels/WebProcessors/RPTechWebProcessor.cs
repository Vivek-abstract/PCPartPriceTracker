using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.WebProcessors
{
    class RPTechWebProcessor : IWebProcessor
    {
        public string Url { get; set; }

        public RPTechWebProcessor(string url)
        {
            Url = url;
        }

        public async Task<double> GetPrice()
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(Url);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//html/body/div[1]/main/div[3]/div/section/div/div[1]/div[2]/div/div[2]/div/div[2]/span[1]/span/div/span/span/span");

            if (double.TryParse(node.InnerText.Trim(new char[] { '₹' }), out double price))
            {
                return price;
            } 
            else
            {
                throw new HtmlWebException("Price not present");
            }
        }
    }
}
