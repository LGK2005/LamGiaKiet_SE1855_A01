using System.Windows.Controls;
using LamGiaKietWPF.ViewModels;

namespace LamGiaKietWPF.Views
{
    public partial class ProductCatalogView : UserControl
    {
        public ProductCatalogView()
        {
            InitializeComponent();
            DataContext = new ProductCatalogViewModel();
        }
    }
} 