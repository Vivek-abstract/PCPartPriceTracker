using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.Commands
{
    public class RefreshProductsCommand : ICommand
    {
        public MainVM VM { get; }

        public event EventHandler CanExecuteChanged;

        private bool _loading;
        public bool Loading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public RefreshProductsCommand(MainVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return !Loading;
        }

        public async void Execute(object parameter)
        {
            Loading = true;
            await VM.RefreshPricesAsync();
            Loading = false;
        }
    }
}
