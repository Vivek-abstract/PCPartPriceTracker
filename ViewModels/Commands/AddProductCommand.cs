using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.Helpers;

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

        public event EventHandler<InvalidInputEventArgs> OnInvalidInput;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Product product = parameter as Product;
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                OnInvalidInput?.Invoke(this, new InvalidInputEventArgs("Name cannot be empty"));
            }
            else if (string.IsNullOrWhiteSpace(product.Url))
            {
                OnInvalidInput?.Invoke(this, new InvalidInputEventArgs("URL cannot be empty"));
            }
            else if (!UrlValidator.IsValid(product.Url))
            {
                OnInvalidInput?.Invoke(this, new InvalidInputEventArgs("URL not supported yet. Kindly use the supported websites"));
            }
            else if (product.TargetPrice == 0)
            {
                OnInvalidInput?.Invoke(this, new InvalidInputEventArgs("Target price cannot be 0. There is an infinitesimal chance you will get the product for free!"));
            }    
            else
            {
                VM.AddProduct();
            }
        }
    }
}
