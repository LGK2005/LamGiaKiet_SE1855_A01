using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using LamGiaKietWPF.Helpers;
using System.ComponentModel;
using System.Windows;

namespace LamGiaKietWPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICustomerService _customerService;
        
        private string _username = string.Empty;
        private string _customerPhone = string.Empty;
        private string _errorMessage = string.Empty;
        private string _customerErrorMessage = string.Empty;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string CustomerPhone
        {
            get => _customerPhone;
            set
            {
                _customerPhone = value;
                OnPropertyChanged(nameof(CustomerPhone));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public string CustomerErrorMessage
        {
            get => _customerErrorMessage;
            set
            {
                _customerErrorMessage = value;
                OnPropertyChanged(nameof(CustomerErrorMessage));
            }
        }

        public LoginViewModel()
        {
            _employeeService = new EmployeeService(AppConfig.ConnectionString);
            _customerService = new CustomerService(AppConfig.ConnectionString);
        }

        public async Task<bool> LoginAsync(string password)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Username and password are required.";
                return false;
            }

            var result = await _employeeService.LoginAsync(Username, password);
            if (result.Success)
            {
                ErrorMessage = string.Empty;
                return true;
            }
            else
            {
                ErrorMessage = result.Message ?? "Invalid credentials.";
                return false;
            }
        }

        public async Task<bool> CustomerLoginAsync(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                CustomerErrorMessage = "Phone number is required.";
                return false;
            }

            var result = await _customerService.LoginByPhoneAsync(phone);
            if (result.Success)
            {
                CustomerErrorMessage = string.Empty;
                return true;
            }
            else
            {
                CustomerErrorMessage = result.Message ?? "Customer not found.";
                return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
} 