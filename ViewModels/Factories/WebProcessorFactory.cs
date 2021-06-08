using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.WebProcessors;

namespace ViewModels.Factories
{
    public static class WebProcessorFactory
    {
        public static IWebProcessor Create(string url)
        {
            if (url.ToLower().Contains("rptechindia.in"))
            {
                return new RPTechWebProcessor(url);
            } 
            else if (url.ToLower().Contains("vedantcomputers.com"))
            {
                return new VedantComputersWebProcessor(url);
            }
            else if (url.ToLower().Contains("mdcomputers.in"))
            {
                return new MDComputersWebProcessor(url);
            }
            else
            {  
                throw new ArgumentException("Url not supported by application yet.");
            }
        }
    }
}
