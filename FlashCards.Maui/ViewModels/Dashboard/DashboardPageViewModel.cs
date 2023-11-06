using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashCards.Contracts.FlashCards;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Entities.Users;
using FlashCards.Maui.Managers.Interfaces;
using System.Collections.ObjectModel;

namespace FlashCards.Maui.ViewModels.Dashboard
{
    public partial class DashboardPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<FlashCard> _flashCards = new();

        [ObservableProperty]
        private int _page = 0;

        private readonly IFlashCardManager _flashCardManager;

        public DashboardPageViewModel(IFlashCardManager flashCardManager) 
        {
            _flashCardManager = flashCardManager;
        }

        public async Task LoadFlashCardsAsync()
        {
            await ExecuteAsync(async () =>
            {
                var flashCards = await _flashCardManager.GetFilteredFlashCards(new GetFilteredFlashCardsRequest<FlashCard>(fc => fc.IsShared == true, Page, 20));
                Page += 1;
                FlashCards = new ObservableCollection<FlashCard>(flashCards.Select(fc => new FlashCard()
                {
                    Id = fc.Id,
                    Word = fc.Word,
                    WordTranslation = fc.WordTranslation,
                    Description = fc.Description,
                    UserId = fc.UserId,
                    User = new UserBasicInfo { Username = fc.Username },
                    IsShared = fc.IsShared,
                }));
            }, "Loading Flashcards...");
        }

        private async Task ExecuteAsync(Func<Task> operation, string? isBusyText = null)
        {
            IsBusy = !IsRefreshing && true;
            IsBusyText = isBusyText ?? "Processing...";
            try
            {
                await operation.Invoke();
            }
            finally
            {
                IsBusy = false;
                IsBusyText = isBusyText ?? "Processing...";
            }
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            IsRefreshing = true;
            Page = 0;
            await LoadFlashCardsAsync();
            IsRefreshing = false;
        }
    }
}
