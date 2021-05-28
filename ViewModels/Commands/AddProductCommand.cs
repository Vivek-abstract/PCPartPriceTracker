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
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.AddProduct();
        }
    }
}
