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
using System.Windows.Shapes;

namespace LamGiaKietWPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void ViewProducts_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProductCatalogView());
        }

        private void MyOrders_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new MyOrdersView());
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new PlaceOrderView());
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProfileView());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
