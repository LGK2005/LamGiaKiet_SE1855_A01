using DataAccessLayer.Models;
using LamGiaKietWPF.Helpers;
using System.ComponentModel;
using BusinessLogicLayer;

namespace LamGiaKietWPF.Dialogs.ViewModels
{
    // ViewModel for Product Add/Edit dialog, implements validation logic
    public class ProductDialogViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; } = string.Empty;
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public int? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public ProductDialogViewModel() { }
        public ProductDialogViewModel(Product product)
        {
            ProductID = product.ProductID;
            ProductName = product.ProductName;
            CategoryID = product.CategoryID;
            QuantityPerUnit = product.QuantityPerUnit;
            UnitPrice = product.UnitPrice;
            UnitsInStock = product.UnitsInStock;
            UnitsOnOrder = product.UnitsOnOrder;
            ReorderLevel = product.ReorderLevel;
            Discontinued = product.Discontinued;
        }

        public Product ToProduct() => new Product
        {
            ProductID = this.ProductID,
            ProductName = this.ProductName,
            CategoryID = this.CategoryID,
            QuantityPerUnit = this.QuantityPerUnit,
            UnitPrice = this.UnitPrice,
            UnitsInStock = this.UnitsInStock,
            UnitsOnOrder = this.UnitsOnOrder,
            ReorderLevel = this.ReorderLevel,
            Discontinued = this.Discontinued
        };

        public string Error => null;
        public string this[string columnName]
        {
            get { return ValidationHelper.ValidateProductProperty(columnName, this); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
} 