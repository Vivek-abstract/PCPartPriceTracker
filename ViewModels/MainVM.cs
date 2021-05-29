using PCPartPriceTracker.Data;
using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Factories;

namespace ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {

        public ObservableCollection<Product> Products { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ProductContext _context = new ProductContext();

        public ProductContext Context { get { return _context; } }

        public MainVM()
        {
            Products = _context.Products.Local.ToObservableCollection();
            InitializeProductPrices();
        }

        public async void InitializeProductPrices()
        {
            foreach (Product product in _context.Products)
            {
                product.Price = await WebProcessorFactory.Create(product.Url).GetPrice();
                _context.SaveChanges();
            }
        }

        public async void RefreshPrices(object sender, EventArgs e)
        {
            foreach (Product product in _context.Products)
            {
                product.Price = await WebProcessorFactory.Create(product.Url).GetPrice();
                _context.SaveChanges();
            }
        }
    }
}
