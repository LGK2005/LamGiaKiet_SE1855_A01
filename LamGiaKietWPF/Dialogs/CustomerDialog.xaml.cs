using System.Windows;

namespace LamGiaKietWPF.Dialogs
{
    public partial class CustomerDialog : Window
    {
        public CustomerDialog()
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