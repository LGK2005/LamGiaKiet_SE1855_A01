using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using LamGiaKietWPF.Dialogs;
using LamGiaKietWPF.Dialogs.ViewModels;
using LamGiaKietWPF.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace LamGiaKietWPF.ViewModels
{
    public class CustomerManagementViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerService _customerService;
        public ObservableCollection<Customer> Customers { get; set; } = new();
        public string SearchKeyword { get; set; } = string.Empty;
        private Customer? _selectedCustomer;
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(nameof(SelectedCustomer)); }
        }

        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        public CustomerManagementViewModel()
        {
            _customerService = new CustomerService(AppConfig.ConnectionString);
            LoadCommand = new RelayCommand(LoadCustomers);
            AddCommand = new RelayCommand(AddCustomer);
            EditCommand = new RelayCommand(EditCustomer, () => SelectedCustomer != null);
            DeleteCommand = new RelayCommand(DeleteCustomer, () => SelectedCustomer != null);
            SearchCommand = new RelayCommand(SearchCustomers);
            LoadCustomers();
        }

        public async void LoadCustomers()
        {
            var result = await _customerService.GetAllCustomersAsync();
            Customers.Clear();
            if (result.Success && result.Data != null)
                foreach (var c in result.Data) Customers.Add(c);
        }

        public void AddCustomer()
        {
            var dialogVM = new CustomerDialogViewModel();
            var dialog = new CustomerDialog { DataContext = dialogVM };
            if (dialog.ShowDialog() == true)
            {
                var result = _customerService.AddCustomerAsync(dialogVM.ToCustomer()).Result;
                if (result.Success) LoadCustomers();
                else MessageBox.Show(result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EditCustomer()
        {
            if (SelectedCustomer == null) return;
            var dialogVM = new CustomerDialogViewModel(SelectedCustomer);
            var dialog = new CustomerDialog { DataContext = dialogVM };
            if (dialog.ShowDialog() == true)
            {
                var result = _customerService.UpdateCustomerAsync(dialogVM.ToCustomer()).Result;
                if (result.Success) LoadCustomers();
                else MessageBox.Show(result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteCustomer()
        {
            if (SelectedCustomer == null) return;
            if (MessageBox.Show($"Delete customer {SelectedCustomer.CompanyName}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = _customerService.DeleteCustomerAsync(SelectedCustomer.CustomerID).Result;
                if (result.Success) LoadCustomers();
                else MessageBox.Show(result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SearchCustomers()
        {
            var result = _customerService.SearchCustomersAsync(SearchKeyword).Result;
            Customers.Clear();
            if (result.Success && result.Data != null)
                foreach (var c in result.Data) Customers.Add(c);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
