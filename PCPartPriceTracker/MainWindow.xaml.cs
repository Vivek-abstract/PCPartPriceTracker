using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels;
using ViewModels.Factories;
using ViewModels.WebProcessors;

namespace PCPartPriceTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVM vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = (MainVM) FindResource("vm");
            //string url = "https://rptechindia.in/asus-graphics-card-dualgtx1660so6gmin.html";
            //IWebProcessor processor = WebProcessorFactory.Create(url);
            //var task = processor.GetPrice();
            //task.Wait();
            //double price = task.Result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewItemVM newItemVM = new NewItemVM();
            NewItemWindow window = new NewItemWindow { DataContext= newItemVM };
            newItemVM.OnProductAdded += vm.RefreshPrices;
            newItemVM.OnRequestClose += (s, e) => window.Close();
            window.ShowDialog();
        }
    }
}
