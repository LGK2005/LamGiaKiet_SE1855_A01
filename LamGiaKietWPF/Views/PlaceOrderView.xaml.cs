using System.Windows;
using System.Windows.Controls;
using LamGiaKietWPF.ViewModels;
using LamGiaKietWPF.Helpers;

namespace LamGiaKietWPF.Views
{
    public partial class PlaceOrderView : UserControl
    {
        private PlaceOrderViewModel _viewModel;

        public PlaceOrderView()
        {
            InitializeComponent();
            
            // Get current customer ID from session
            var customerId = SessionManager.CurrentCustomer?.CustomerID ?? 0;
            _viewModel = new PlaceOrderViewModel(customerId);
            DataContext = _viewModel;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddToCart();
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int productId)
            {
                _viewModel.RemoveFromCart(productId);
            }
        }

        private void ClearCart_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to clear your cart?", "Confirm", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                _viewModel.ClearCart();
            }
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to place this order?", "Confirm Order", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                if (_viewModel.PlaceOrder())
                {
                    // Order placed successfully, could navigate to order confirmation or orders list
                    MessageBox.Show("Your order has been placed successfully!", "Order Confirmation", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
} 