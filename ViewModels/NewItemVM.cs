using PCPartPriceTracker.Data;
using PCPartPriceTracker.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewModels.Commands;
using ViewModels.Helpers;
using System.Windows;
using ViewModels.Factories;

namespace ViewModels
{
    public class NewItemVM : INotifyPropertyChanged
    {
        private ProductContext _context = new ProductContext();
        public event EventHandler OnRequestClose;
        public event EventHandler OnProductAdded;

        public Product Product { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value;
                Product.Name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _url;

        public string Url
        {
            get { return _url; }
            set 
            { 
                _url = value;
                Product.Url = value;
                OnPropertyChanged("Url");
            }
        }

        private double _targetPrice;

        public double TargetPrice
        {
            get { return _targetPrice; }
            set
            {
                _targetPrice = value;
                Product.TargetPrice = value;
                OnPropertyChanged("TargetPrice");
            }
        }

        public event EventHandler<InvalidInputEventArgs> OnInvalidInput;
        public AddProductCommand AddProductCommand { get; set; }

        public NewItemVM()
        {
            Product = new Product();
            AddProductCommand = new AddProductCommand(this);
            AddProductCommand.OnInvalidInput += AddProductCommand_OnInvalidInput;
        }

        private void AddProductCommand_OnInvalidInput(object sender, InvalidInputEventArgs e)
        {
            OnInvalidInput?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddProduct()
        {
            Product product = new Product { Name = Name, Url = Url, TargetPrice = TargetPrice };

            _context.Add(Product);
            _context.SaveChanges();

            OnProductAdded?.Invoke(this, EventArgs.Empty);
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
