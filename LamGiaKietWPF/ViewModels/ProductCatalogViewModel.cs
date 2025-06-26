using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using LamGiaKietWPF.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace LamGiaKietWPF.ViewModels
{
    public class ProductCatalogViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _productService;
        public ObservableCollection<Product> Products { get; set; } = new();
        public string SearchKeyword { get; set; } = string.Empty;
        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }

        public ICommand LoadCommand { get; }
        public ICommand SearchCommand { get; }

        public ProductCatalogViewModel()
        {
            _productService = new ProductService(AppConfig.ConnectionString);
            LoadCommand = new RelayCommand(async () => await LoadProducts());
            SearchCommand = new RelayCommand(async () => await SearchProducts());
            _ = LoadProducts();
        }

        public async Task LoadProducts()
        {
            var result = await _productService.GetAllProductsAsync();
            Products.Clear();
            if (result.Success && result.Data != null)
                foreach (var p in result.Data) Products.Add(p);
        }

        public async Task SearchProducts()
        {
            var result = await _productService.SearchProductsAsync(SearchKeyword);
            Products.Clear();
            if (result.Success && result.Data != null)
                foreach (var p in result.Data) Products.Add(p);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
} 