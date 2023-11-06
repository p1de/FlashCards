using CommunityToolkit.Mvvm.ComponentModel;

namespace FlashCards.Maui.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty] 
        private string _isBusyText;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private string _title;
    }
}