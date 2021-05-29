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
            else
            {  
                throw new ArgumentException("Url not supported by application yet.");
            }
        }
    }
}
