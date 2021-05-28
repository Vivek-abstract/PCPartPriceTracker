using PCPartPriceTracker.Data;
using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {

        public ObservableCollection<Product> Products { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ProductContext _context = new ProductContext();

        public MainVM()
        {
            Products = new ObservableCollection<Product>(_context.Products);
        }

    }
}
