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
        private List<Product> _allProducts = new();
        
        public ObservableCollection<Product> Products { get; set; } = new();
        
        private string _searchKeyword = string.Empty;
        public string SearchKeyword 
        { 
            get => _searchKeyword;
            set 
            { 
                _searchKeyword = value; 
                OnPropertyChanged(nameof(SearchKeyword));
                FilterProducts();
            }
        }
        
        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }

        public ICommand LoadCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }

        public ProductCatalogViewModel()
        {
            _productService = new ProductService(AppConfig.ConnectionString);
            LoadCommand = new AsyncRelayCommand(LoadProductsAsync);
            SearchCommand = new AsyncRelayCommand(SearchProductsAsync);
            ClearSearchCommand = new RelayCommand(ClearSearch);
            _ = LoadProductsAsync();
        }

        public async Task LoadProductsAsync()
        {
            var result = await _productService.GetAllProductsAsync();
            if (result.Success && result.Data != null)
            {
                _allProducts = result.Data;
                Products.Clear();
                foreach (var product in _allProducts)
                {
                    Products.Add(product);
                }
            }
        }

        public async Task SearchProductsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchKeyword))
            {
                await LoadProductsAsync();
            }
            else
            {
                var result = await _productService.SearchProductsAsync(SearchKeyword);
                if (result.Success && result.Data != null)
                {
                    Products.Clear();
                    foreach (var product in result.Data)
                    {
                        Products.Add(product);
                    }
                }
            }
        }

        public void ClearSearch()
        {
            SearchKeyword = string.Empty;
            _ = LoadProductsAsync();
        }

        private void FilterProducts()
        {
            if (string.IsNullOrWhiteSpace(SearchKeyword))
            {
                // Show all products
                Products.Clear();
                foreach (var product in _allProducts)
                {
                    Products.Add(product);
                }
            }
            else
            {
                // Filter products based on search keyword
                var filteredProducts = _allProducts.Where(p =>
                    p.ProductName?.Contains(SearchKeyword, System.StringComparison.OrdinalIgnoreCase) == true ||
                    p.ProductID.ToString().Contains(SearchKeyword, System.StringComparison.OrdinalIgnoreCase)
                ).ToList();

                Products.Clear();
                foreach (var product in filteredProducts)
                {
                    Products.Add(product);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
} 