using System;
using System.ComponentModel;
using System.Windows;
using BusinessLogicLayer.IServices;
using DataAccessLayer.Models;
using LamGiaKietWPF.Helpers;

namespace LamGiaKietWPF.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerService _customerService;
        private Customer? _originalCustomer;
        
        private int _customerId;
        private string _companyName = string.Empty;
        private string _contactName = string.Empty;
        private string _contactTitle = string.Empty;
        private string _address = string.Empty;

        public ProfileViewModel()
        {
            _customerService = new BusinessLogicLayer.Services.CustomerService();
            LoadCustomerProfile();
        }

        public int CustomerID
        {
            get => _customerId;
            set
            {
                _customerId = value;
                OnPropertyChanged(nameof(CustomerID));
            }
        }

        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        public string ContactName
        {
            get => _contactName;
            set
            {
                _contactName = value;
                OnPropertyChanged(nameof(ContactName));
            }
        }

        public string ContactTitle
        {
            get => _contactTitle;
            set
            {
                _contactTitle = value;
                OnPropertyChanged(nameof(ContactTitle));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private void LoadCustomerProfile()
        {
            try
            {
                var currentCustomer = SessionManager.CurrentCustomer;
                if (currentCustomer == null)
                {
                    MessageBox.Show("No customer session found. Please log in again.", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var result = _customerService.GetCustomerById(currentCustomer.CustomerID);
                
                if (result.Success && result.Data != null)
                {
                    _originalCustomer = result.Data;
                    
                    CustomerID = _originalCustomer.CustomerID;
                    CompanyName = _originalCustomer.CompanyName ?? "";
                    ContactName = _originalCustomer.ContactName ?? "";
                    ContactTitle = _originalCustomer.ContactTitle ?? "";
                    Address = _originalCustomer.Address ?? "";
                }
                else
                {
                    MessageBox.Show($"Error loading profile: {result.Message}", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool SaveChanges()
        {
            try
            {
                if (_originalCustomer == null)
                {
                    MessageBox.Show("No customer data to update.", "Error", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Update the customer object with new values
                _originalCustomer.CompanyName = CompanyName;
                _originalCustomer.ContactName = ContactName;
                _originalCustomer.ContactTitle = ContactTitle;
                _originalCustomer.Address = Address;

                var result = _customerService.UpdateCustomer(_originalCustomer);
                
                if (result.Success)
                {
                    // Update the session with the new customer data
                    SessionManager.CurrentCustomer = _originalCustomer;
                    
                    MessageBox.Show("Profile updated successfully!", "Success", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error updating profile: {result.Message}", "Error", 
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

        public void CancelChanges()
        {
            // Reload the original data
            LoadCustomerProfile();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 