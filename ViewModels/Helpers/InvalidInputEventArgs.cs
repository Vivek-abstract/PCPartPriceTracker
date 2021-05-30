using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Helpers
{
    public class InvalidInputEventArgs : EventArgs
    {
        public string Error { get; set; }

        public InvalidInputEventArgs(string error)
        {
            Error = error;
        }

    }
}
