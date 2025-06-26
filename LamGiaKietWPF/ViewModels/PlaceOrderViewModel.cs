using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.IServices;
using DataAccessLayer.Models;

namespace LamGiaKietWPF.ViewModels
{
    public class PlaceOrderViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly int _customerId;
        
        private ObservableCollection<Product> _availableProducts;
        private ObservableCollection<CartItem> _cartItems;
        private Product _selectedProduct;
        private string _quantity;
        private string _shippingAddress;
        private string _orderNotes;

        public PlaceOrderViewModel(int customerId)
        {
            _productService = new BusinessLogicLayer.Services.ProductService();
            _orderService = new BusinessLogicLayer.Services.OrderService();
            _customerId = customerId;
            
            AvailableProducts = new ObservableCollection<Product>();
            CartItems = new ObservableCollection<CartItem>();
            Quantity = "1";
            ShippingAddress = "";
            OrderNotes = "";
            
            LoadProducts();
        }

        public ObservableCollection<Product> AvailableProducts
        {
            get => _availableProducts;
            set
            {
                _availableProducts = value;
                OnPropertyChanged(nameof(AvailableProducts));
            }
        }

        public ObservableCollection<CartItem> CartItems
        {
            get => _cartItems;
            set
            {
                _cartItems = value;
                OnPropertyChanged(nameof(CartItems));
                OnPropertyChanged(nameof(CartItemCount));
                OnPropertyChanged(nameof(Subtotal));
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                OnPropertyChanged(nameof(SelectedProductPrice));
            }
        }

        public string Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public string ShippingAddress
        {
            get => _shippingAddress;
            set
            {
                _shippingAddress = value;
                OnPropertyChanged(nameof(ShippingAddress));
            }
        }

        public string OrderNotes
        {
            get => _orderNotes;
            set
            {
                _orderNotes = value;
                OnPropertyChanged(nameof(OrderNotes));
            }
        }

        public string SelectedProductPrice
        {
            get
            {
                if (SelectedProduct != null)
                {
                    return SelectedProduct.UnitPrice?.ToString("C") ?? "$0.00";
                }
                return "$0.00";
            }
        }

        public int CartItemCount => CartItems.Count;

        public decimal Subtotal => CartItems.Sum(item => item.Total);

        public decimal TotalAmount => Subtotal; // No tax or shipping for now

        private void LoadProducts()
        {
            try
            {
                var result = _productService.GetAllProducts();
                
                if (result.Success)
                {
                    AvailableProducts.Clear();
                    foreach (var product in result.Data)
                    {
                        AvailableProducts.Add(product);
                    }
                }
                else
                {
                    MessageBox.Show($"Error loading products: {result.Message}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddToCart()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a product.", "Warning", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(Quantity, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Warning", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Check if product is already in cart
            var existingItem = CartItems.FirstOrDefault(item => item.ProductID == SelectedProduct.ProductID);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.Total = existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductID = SelectedProduct.ProductID,
                    ProductName = SelectedProduct.ProductName,
                    Quantity = quantity,
                    UnitPrice = SelectedProduct.UnitPrice ?? 0,
                    Total = quantity * (SelectedProduct.UnitPrice ?? 0)
                };
                CartItems.Add(cartItem);
            }

            // Reset selection
            SelectedProduct = null;
            Quantity = "1";
            
            OnPropertyChanged(nameof(CartItems));
        }

        public void RemoveFromCart(int productId)
        {
            var item = CartItems.FirstOrDefault(item => item.ProductID == productId);
            if (item != null)
            {
                CartItems.Remove(item);
                OnPropertyChanged(nameof(CartItems));
            }
        }

        public void ClearCart()
        {
            CartItems.Clear();
            OnPropertyChanged(nameof(CartItems));
        }

        public bool PlaceOrder()
        {
            if (CartItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty. Please add some products.", "Warning", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(ShippingAddress))
            {
                MessageBox.Show("Please enter a shipping address.", "Warning", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            try
            {
                var order = new Order
                {
                    CustomerID = _customerId,
                    OrderDate = DateTime.Now
                };

                var result = _orderService.CreateOrder(order, CartItems.ToList());
                
                if (result.Success)
                {
                    MessageBox.Show($"Order placed successfully! Order ID: {result.Data}", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearCart();
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error placing order: {result.Message}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 