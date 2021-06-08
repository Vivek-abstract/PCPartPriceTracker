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
    public class EditProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private EditItemVM VM;

        public EditProductCommand(EditItemVM vm)
        {
            VM = vm;
            VM.PropertyChanged += (s, e) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<InvalidInputEventArgs> OnInvalidInput;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(VM.Name))
            {
                OnInvalidInput?.Invoke(this, new InvalidInputEventArgs("Name cannot be empty"));
            }
            else if (string.IsNullOrWhiteSpace(VM.Url))
            {
                OnInvalidInput?.Invoke(this, new InvalidInputEventArgs("URL cannot be empty"));
            }
            else if (!UrlValidator.IsValid(VM.Url))
            {
                OnInvalidInput?.Invoke(this, new InvalidInputEventArgs("URL not supported yet. Kindly use the supported websites"));
            }
            else if (VM.TargetPrice == 0)
            {
                OnInvalidInput?.Invoke(this, new InvalidInputEventArgs("Target price cannot be 0. There is an infinitesimal chance you will get the product for free!"));
            }
            else
            {
                VM.EditProduct();
            }
        }
    }
}
