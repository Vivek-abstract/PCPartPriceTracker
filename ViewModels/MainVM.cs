using Microsoft.EntityFrameworkCore;
using PCPartPriceTracker.Data;
using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModels.Commands;
using ViewModels.Factories;

namespace ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {

        public ObservableCollection<Product> Products { get; set; }

        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                OnPropertyChanged("Loading");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ProductContext _context = new ProductContext();

        public ProductContext Context { get { return _context; } }

        public RefreshProductsCommand RefreshProductsCommand { get; set; }

        public MainVM()
        {
            _context.Database.Migrate();
            Products = new ObservableCollection<Product>();
            RefreshProductsCommand = new RefreshProductsCommand(this);
            PopulateProducts();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PopulateProducts()
        {
            Products.Clear();
            foreach (Product product in _context.Products)
            {
                Products.Add(product);
            }
        }

        public async Task RefreshPricesAsync()
        {
            Loading = true;
            var products = await _context.Products.ToListAsync();
            var tasks = new List<Task<Product>>();

            foreach (Product product in products)
            {
                Task<Product> productTask = WebProcessorFactory.Create(product.Url).GetProductStockAndPrice(product);
                tasks.Add(productTask);
            }

            Product[] updatedProducts = await Task.WhenAll(tasks);
            foreach (Product product in products)
            {
                var updatedProduct = updatedProducts.Where(p => p.Id == product.Id).SingleOrDefault();
                product.Price = updatedProduct.Price;
                product.InStock = updatedProduct.InStock;
            }
            await _context.SaveChangesAsync();

            Products.Clear();
            foreach (Product product in products.OrderBy(x => x.PriceColor).ThenBy(x => x.Price))
            {
                Products.Add(product);
            }
            Loading = false;
        }
    }
}
