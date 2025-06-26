using System.ComponentModel;

namespace LamGiaKietWPF.Dialogs.ViewModels
{
    // ViewModel for Order Add/Edit dialog, implements validation logic
    public class OrderDialogViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        // TODO: Add properties for Order fields (Customer, Products, Discount, etc.)
        // TODO: Implement validation logic
        public string Error => null;
        public string this[string columnName] => null;
        public event PropertyChangedEventHandler PropertyChanged;
    }
} 