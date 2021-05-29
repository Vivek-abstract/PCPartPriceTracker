using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class AddProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public NewItemVM VM { get; set; }

        public AddProductCommand(NewItemVM vm)
        {
            VM = vm;

            // Check if button can be enabled once property changes
            VM.PropertyChanged += (s, e) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            Product product = parameter as Product;
            if (product != null
                && !string.IsNullOrWhiteSpace(product.Name)
                && product.TargetPrice != 0
                && IsValidUrl(product.Url))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsValidUrl(string url)
        {
            if(url.Contains("rptechindia.in") 
                || url.Contains("vedantcomputers.com"))
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            VM.AddProduct();
        }
    }
}
