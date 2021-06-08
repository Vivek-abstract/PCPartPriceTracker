using PCPartPriceTracker.Data;
using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class DeleteProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ProductContext _context = new ProductContext();

        public EditItemVM Vm { get; }

        public DeleteProductCommand(EditItemVM vm)
        {
            Vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Vm.DeleteProduct();
        }
    }
}
