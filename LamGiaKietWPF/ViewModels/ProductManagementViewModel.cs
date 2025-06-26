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
    public class ProductManagementViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;
        public ObservableCollection<Product> Products { get; set; } = new();
        public string SearchKeyword { get; set; } = string.Empty;
        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }

        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        public ProductManagementViewModel()
        {
            _productService = new ProductService(AppConfig.ConnectionString);
            LoadCommand = new RelayCommand(LoadProducts);
            AddCommand = new RelayCommand(AddProduct);
            EditCommand = new RelayCommand(EditProduct, () => SelectedProduct != null);
            DeleteCommand = new RelayCommand(DeleteProduct, () => SelectedProduct != null);
            SearchCommand = new RelayCommand(SearchProducts);
            LoadProducts();
        }

        public async void LoadProducts()
        {
            var result = await _productService.GetAllProductsAsync();
            Products.Clear();
            if (result.Success && result.Data != null)
                foreach (var p in result.Data) Products.Add(p);
        }

        public void AddProduct()
        {
            var dialogVM = new ProductDialogViewModel();
            var dialog = new ProductDialog { DataContext = dialogVM };
            if (dialog.ShowDialog() == true)
            {
                var result = _productService.AddProductAsync(dialogVM.ToProduct()).Result;
                if (result.Success) LoadProducts();
                else MessageBox.Show(result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EditProduct()
        {
            if (SelectedProduct == null) return;
            var dialogVM = new ProductDialogViewModel(SelectedProduct);
            var dialog = new ProductDialog { DataContext = dialogVM };
            if (dialog.ShowDialog() == true)
            {
                var result = _productService.UpdateProductAsync(dialogVM.ToProduct()).Result;
                if (result.Success) LoadProducts();
                else MessageBox.Show(result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteProduct()
        {
            if (SelectedProduct == null) return;
            if (MessageBox.Show($"Delete product {SelectedProduct.ProductName}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = _productService.DeleteProductAsync(SelectedProduct.ProductID).Result;
                if (result.Success) LoadProducts();
                else MessageBox.Show(result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SearchProducts()
        {
            var result = _productService.SearchProductsAsync(SearchKeyword).Result;
            Products.Clear();
            if (result.Success && result.Data != null)
                foreach (var p in result.Data) Products.Add(p);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
