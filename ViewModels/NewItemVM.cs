using PCPartPriceTracker.Data;
using PCPartPriceTracker.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewModels.Commands;

namespace ViewModels
{
    public class NewItemVM : INotifyPropertyChanged
    {
        private Product _product;
        private ProductContext _context = new ProductContext();
        public event EventHandler OnRequestClose;
        public Product Product
        {
            get { return _product; }
            set 
            { 
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        public AddProductCommand AddProductCommand { get; set; }


        public NewItemVM()
        {
            //Product = new Product { Name = "Vivek", Url = "a.b.com", TargetPrice = 123 };
            Product = new Product();
            AddProductCommand = new AddProductCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddProduct()
        {
            _context.Add(Product);
            _context.SaveChanges();

            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
