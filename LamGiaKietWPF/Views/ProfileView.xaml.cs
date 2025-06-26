using System.Windows;
using System.Windows.Controls;
using LamGiaKietWPF.ViewModels;

namespace LamGiaKietWPF.Views
{
    public partial class ProfileView : UserControl
    {
        private ProfileViewModel _viewModel;

        public ProfileView()
        {
            InitializeComponent();
            _viewModel = new ProfileViewModel();
            DataContext = _viewModel;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveChanges();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel? All unsaved changes will be lost.", 
                "Confirm Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                _viewModel.CancelChanges();
            }
        }
    }
} 