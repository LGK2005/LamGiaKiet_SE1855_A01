using System.Windows;

namespace LamGiaKietWPF.Dialogs
{
    public partial class ProductDialog : Window
    {
        public ProductDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 