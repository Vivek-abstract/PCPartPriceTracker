using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Helpers
{
    public static class UrlValidator
    {
        public static bool IsValid(string url)
        {
            if (!string.IsNullOrWhiteSpace(url) && (url.Contains("rptechindia.in")
                || url.Contains("vedantcomputers.com")
                || url.Contains("mdcomputers.in")
                || url.Contains("www.primeabgb.com")))
            {
                return true;
            }
            return false;
        }
    }
}
