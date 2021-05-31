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
            Products = new ObservableCollection<Product>();
            RefreshPrices(this, EventArgs.Empty);
        }

        public void RefreshPrices(object sender, EventArgs e)
        {
            var products = _context.Products.ToList();
            Products.Clear();

            foreach (Product product in products)
            {
                Product updatedProduct = WebProcessorFactory.Create(product.Url).GetProductStockAndPrice(product);
                product.Price = updatedProduct.Price;
                product.InStock = updatedProduct.InStock;
                _context.SaveChanges();
            }

            foreach (Product product in products.OrderBy(x => x.PriceColor).ThenBy(x=>x.Price))
            {
                Products.Add(product);
            }
        }
    }
}
