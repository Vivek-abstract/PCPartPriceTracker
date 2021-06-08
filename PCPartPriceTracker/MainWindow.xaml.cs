using PCPartPriceTracker.Models;
using PCPartPriceTracker.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels;
using ViewModels.Commands;
using ViewModels.Factories;
using ViewModels.WebProcessors;

namespace PCPartPriceTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVM VM;
        private static Timer refreshTimer;
        public MainWindow()
        {
            InitializeComponent();
            VM = (MainVM)FindResource("vm");
            SetupAutoRefresh();
            //string url = "https://www.vedantcomputers.com/gigabyte-geforce-gtx-1660-super-oc-6gb-gddr6";
            //IWebProcessor processor = WebProcessorFactory.Create(url);
            //var task = processor.GetPrice();
            //task.Wait();
            //double price = task.Result;
        }
        private void SetupAutoRefresh()
        {
            refreshTimer = new Timer(e =>
            {
                App.Current.Dispatcher.Invoke(async () => await VM.RefreshPricesAsync());
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewItemVM newItemVM = new NewItemVM();
            NewItemWindow window = new NewItemWindow { DataContext = newItemVM };
            newItemVM.OnProductAdded += async (s, e) => await VM.RefreshPricesAsync();
            newItemVM.OnRequestClose += (s, e) => window.Close();
            newItemVM.OnInvalidInput += NewItemVM_OnInvalidInput;
            window.ShowDialog();
        }

        private void NewItemVM_OnInvalidInput(object sender, ViewModels.Helpers.InvalidInputEventArgs e)
        {
            MessageBox.Show(e.Error, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lview = sender as ListView;
            Product product = lview.SelectedItem as Product;

            if (product != null)
            {
                EditItemVM editItemVM = new EditItemVM(product);
                EditItemWindow window = new EditItemWindow { DataContext = editItemVM };
                editItemVM.OnProductEdited += async (s, e) => await VM.RefreshPricesAsync();
                editItemVM.OnRequestClose += (s, e) => window.Close();
                editItemVM.OnInvalidInput += NewItemVM_OnInvalidInput;
                window.ShowDialog();
            }

        }
    }
}
