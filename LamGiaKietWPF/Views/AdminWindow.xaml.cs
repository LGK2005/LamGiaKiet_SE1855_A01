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
using LamGiaKietWPF.ManagementView;

namespace LamGiaKietWPF.Views
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void CustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new CustomerManagementView());
        }

        private void ProductManagement_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProductManagementView());
        }

        private void OrderManagement_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new OrderManagementView());
        }

        private void ReportManagement_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ReportManagementView());
        }

        private void ViewAllOrders_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new OrderManagementView());
        }

        private void ViewAllProducts_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProductManagementView());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
