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
using LamGiaKietWPF.ViewModels;

namespace LamGiaKietWPF.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
        }

        private async void EmployeeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            // For testing purposes, let's bypass the database check for now
            var username = EmployeeUsernameTextBox.Text;
            var password = EmployeePasswordBox.Password;
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            // Simple test - accept any non-empty credentials for now
            var adminWindow = new AdminWindow();
            adminWindow.Show();
            Close();
        }

        private async void CustomerLoginButton_Click(object sender, RoutedEventArgs e)
        {
            // For testing purposes, let's bypass the database check for now
            var phone = CustomerPhoneTextBox.Text;
            
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please enter a phone number.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            // Simple test - accept any non-empty phone for now
            var customerWindow = new CustomerWindow();
            customerWindow.Show();
            Close();
        }
    }
}
