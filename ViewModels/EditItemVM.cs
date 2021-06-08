using PCPartPriceTracker.Data;
using PCPartPriceTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Commands;
using ViewModels.Helpers;

namespace ViewModels
{
    public class EditItemVM : INotifyPropertyChanged
    {
        public event EventHandler OnProductEdited;
        public event EventHandler OnRequestClose;
        public event EventHandler<InvalidInputEventArgs> OnInvalidInput;
        public event PropertyChangedEventHandler PropertyChanged;
        private ProductContext _context = new ProductContext();
        public Product Product { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
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
                OnPropertyChanged("TargetPrice");
            }
        }

        public EditProductCommand EditProductCommand { get; set; }
        public DeleteProductCommand DeleteProductCommand { get; set; }

        public EditItemVM(Product product)
        {
            Product = product;
            Name = product.Name;
            Url = product.Url;
            TargetPrice = product.TargetPrice;
            EditProductCommand = new EditProductCommand(this);
            DeleteProductCommand = new DeleteProductCommand(this);
            EditProductCommand.OnInvalidInput += Command_OnInvalidInput;
        }

        private void Command_OnInvalidInput(object sender, InvalidInputEventArgs e)
        {
            OnInvalidInput?.Invoke(this, e);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void EditProduct()
        {
            Product.Name = Name;
            Product.Url = Url;
            Product.TargetPrice = TargetPrice;
            _context.SaveChanges();

            OnProductEdited?.Invoke(this, EventArgs.Empty);
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteProduct()
        {
            _context.Products.Remove(Product);
            _context.SaveChanges();
            OnProductEdited?.Invoke(this, EventArgs.Empty);
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

    }
}
