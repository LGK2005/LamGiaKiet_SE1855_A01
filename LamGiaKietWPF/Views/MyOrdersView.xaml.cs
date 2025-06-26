using System.Windows;
using System.Windows.Controls;
using LamGiaKietWPF.ViewModels;
using LamGiaKietWPF.Helpers;

namespace LamGiaKietWPF.Views
{
    public partial class MyOrdersView : UserControl
    {
        private MyOrdersViewModel _viewModel;

        public MyOrdersView()
        {
            InitializeComponent();
            
            // Get current customer ID from session
            var customerId = SessionManager.CurrentCustomer?.CustomerID ?? 0;
            _viewModel = new MyOrdersViewModel(customerId);
            DataContext = _viewModel;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshCommand.Execute(null);
        }

        private void ViewOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int orderId)
            {
                // TODO: Navigate to order details view
                MessageBox.Show($"Viewing details for Order #{orderId}", "Order Details", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
} 