using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashCards.Contracts.FlashCards;
using FlashCards.Core.Application.CQRS.FlashCards.Queries.Get;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Helpers;
using FlashCards.Maui.Managers.Interfaces;
using System.Collections.ObjectModel;

namespace FlashCards.Maui.ViewModels.FlashCards
{
    public partial class FlashCardsPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private FlashCard _operatingFlashCard = new();

        [ObservableProperty]
        private ObservableCollection<FlashCard> _flashCards = new();

        [ObservableProperty]
        private int _page = 0;

        private readonly IFlashCardManager _flashCardManager;

        public FlashCardsPageViewModel(IFlashCardManager flashCardManager)
        {
            _flashCardManager = flashCardManager;
        }

        public async Task LoadFlashCardsAsync()
        {
            await ExecuteAsync(async () =>
            {
                var flashCards = await _flashCardManager.GetFlashCards(new GetFlashCardsRequest(App.UserDetails?.Id, Page, 20));
                Page += 1;
                FlashCards = new ObservableCollection<FlashCard>(flashCards.Select(fc => new FlashCard()
                {
                    Id = fc.Id,
                    Word = fc.Word,
                    WordTranslation = fc.WordTranslation,
                    Description = fc.Description,
                    UserId = fc.UserId,
                    IsShared = fc.IsShared,
                }));
            }, "Loading Flashcards...");
        }

        [RelayCommand]
        public async Task CreateOrUpdateFlashCardAsync()
        {
            if (OperatingFlashCard is null)
                return;

            var isBusyText = OperatingFlashCard.Id == Guid.Empty.ToString() ? "Creating Flashcard..." : "Updating existing Flashcard...";

            await ExecuteAsync(async () =>
            {
                if (OperatingFlashCard.Id == Guid.Empty.ToString())
                {
                    var createdFlashCard = await _flashCardManager.CreateFlashCard(new CreateFlashCardRequest(
                        App.UserDetails.Id,
                        OperatingFlashCard.Word,
                        OperatingFlashCard.WordTranslation,
                        OperatingFlashCard.Description,
                        OperatingFlashCard.Tags?.Select(t => t.Id).ToList()));

                    FlashCards.Add(new FlashCard()
                    {
                        Id = createdFlashCard.Id,
                        UserId = createdFlashCard.UserId,
                        Word = createdFlashCard.Word,
                        WordTranslation = createdFlashCard.WordTranslation,
                        Description = createdFlashCard.Description
                    });
                }
                else
                {
                    await _flashCardManager.UpdateFlashCard(new UpdateFlashCardRequest(
                        OperatingFlashCard.Id,
                        App.UserDetails.Id,
                        OperatingFlashCard.Word,
                        OperatingFlashCard.WordTranslation,
                        OperatingFlashCard.Description,
                        OperatingFlashCard.Tags?.Select(t => t.Id).ToList(),
                        false));

                    var flashCardCopy = OperatingFlashCard.CloneJson();

                    var index = FlashCards.IndexOf(OperatingFlashCard);

                    FlashCards.RemoveAt(index);
                    FlashCards.Insert(index, flashCardCopy);
                }
                SetOperatingFlashCardCommand.Execute(new());
            }, isBusyText);
        }

        [RelayCommand]
        private async Task ShareOrUnshareFlashCardAsync(string id)
        {           
            var flashCard = FlashCards.FirstOrDefault(fc => fc.Id == id);
            var isBusyText = flashCard.IsShared ? "Unsharing Flashcard..." : "Sharing Flashcard...";

            await ExecuteAsync(async () =>
            {
                var response = await _flashCardManager.UpdateFlashCard(new UpdateFlashCardRequest(
                    id,
                    App.UserDetails.Id,
                    flashCard.Word,
                    flashCard.WordTranslation,
                    flashCard.Description,
                    flashCard.Tags?.Select(t => t.Id).ToList(),
                    !flashCard.IsShared));

                flashCard.IsShared = response.IsShared;

                var flashCardCopy = flashCard.CloneJson();

                var index = FlashCards.IndexOf(flashCard);

                FlashCards.RemoveAt(index);
                FlashCards.Insert(index, flashCardCopy);
            }, isBusyText);
        }

        [RelayCommand]
        private async Task DeleteFlashCardAsync(string id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _flashCardManager.DeleteFlashCard(new DeleteFlashCardRequest(id)))
                {
                    var product = FlashCards.FirstOrDefault(p => p.Id == id);
                    FlashCards.Remove(product);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Flashcard was not deleted", "Ok");
                }
            }, "Deleting Flashcard...");
        }

        private async Task ExecuteAsync(Func<Task> operation, string? isBusyText = null)
        {
            IsBusy = true;
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
        private void SetOperatingFlashCard(FlashCard? flashCard) => OperatingFlashCard = flashCard ?? new();
    }
}
