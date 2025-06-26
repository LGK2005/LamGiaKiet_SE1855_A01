using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BusinessLogicLayer.IServices;
using DataAccessLayer.Models;

namespace LamGiaKietWPF.ViewModels
{
    public class MyOrdersViewModel : INotifyPropertyChanged
    {
        private readonly IOrderService _orderService;
        private readonly int _customerId;
        
        private ObservableCollection<Order> _orders;
        private string _searchText;
        private bool _isLoading;
        private bool _isEmpty;

        public MyOrdersViewModel(int customerId)
        {
            _orderService = new BusinessLogicLayer.Services.OrderService();
            _customerId = customerId;
            
            Orders = new ObservableCollection<Order>();
            SearchText = "";
            
            LoadOrders();
        }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterOrders();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                _isEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
            }
        }

        public ICommand RefreshCommand => new RelayCommand(LoadOrders);

        private void LoadOrders()
        {
            IsLoading = true;
            IsEmpty = false;

            try
            {
                var result = _orderService.GetOrdersByCustomerId(_customerId);
                
                if (result.Success)
                {
                    Orders.Clear();
                    foreach (var order in result.Data)
                    {
                        Orders.Add(order);
                    }
                    
                    IsEmpty = Orders.Count == 0;
                }
                else
                {
                    MessageBox.Show($"Error loading orders: {result.Message}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    IsEmpty = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                IsEmpty = true;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void FilterOrders()
        {
            // Filter orders based on search text
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadOrders();
            }
            else
            {
                // For now, just reload all orders since we're using mock data
                // In a real implementation, you would filter the existing orders
                LoadOrders();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object? parameter)
        {
            _execute();
        }
    }
} 