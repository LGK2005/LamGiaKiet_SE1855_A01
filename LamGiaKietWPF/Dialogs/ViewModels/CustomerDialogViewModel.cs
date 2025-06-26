using DataAccessLayer.Models;
using LamGiaKietWPF.Helpers;
using System.ComponentModel;
using BusinessLogicLayer;

namespace LamGiaKietWPF.Dialogs.ViewModels
{
    // ViewModel for Customer Add/Edit dialog, implements validation logic
    public class CustomerDialogViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public int CustomerID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactTitle { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public CustomerDialogViewModel() { }
        public CustomerDialogViewModel(Customer customer)
        {
            CustomerID = customer.CustomerID;
            CompanyName = customer.CompanyName;
            ContactName = customer.ContactName;
            ContactTitle = customer.ContactTitle;
            Address = customer.Address;
            Phone = customer.Phone;
        }

        public Customer ToCustomer() => new Customer
        {
            CustomerID = this.CustomerID,
            CompanyName = this.CompanyName,
            ContactName = this.ContactName,
            ContactTitle = this.ContactTitle,
            Address = this.Address,
            Phone = this.Phone
        };

        public string Error => null;
        public string this[string columnName]
        {
            get { return ValidationHelper.ValidateCustomerProperty(columnName, this); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
} 