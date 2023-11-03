using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashCards.Contracts.FlashCards;
using FlashCards.Core.Application.CQRS.FlashCards.Queries.Get;
using FlashCards.Domain.Entities.FlashCards;
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
                var flashCards = await _flashCardManager.GetFlashCards(new GetFlashCardsQuery(App.UserDetails?.Id, Page, 20));
                Page += 1;
                FlashCards = new ObservableCollection<FlashCard>(flashCards.Select(fc => new FlashCard()
                {
                    Id = fc.Id,
                    Word = fc.Word,
                    WordTranslation = fc.WordTranslation,
                    Description = fc.Description,
                    UserId = fc.UserId
                }));
            }, "Loading Flashcards...");
        }

        [RelayCommand]
        public async Task CreateFlashCardAsync()
        {
            if (OperatingFlashCard is null)
                return;

            var isBusyText = OperatingFlashCard.Id == Guid.Empty.ToString() ? "Creating flashcard..." : "Updating existing flashcard...";

            await ExecuteAsync(async () =>
            {
                if (OperatingFlashCard.Id == Guid.Empty.ToString())
                {
                    var createdFlashCard = await _flashCardManager.CreateFlashCard(new CreateFlashCardRequest(
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
                        OperatingFlashCard.Word,
                        OperatingFlashCard.WordTranslation,
                        OperatingFlashCard.Description,
                        OperatingFlashCard.Tags?.Select(t => t.Id).ToList()));

                    var flashCardCopy = OperatingFlashCard.Clone();

                    var index = FlashCards.IndexOf(OperatingFlashCard);

                    FlashCards.RemoveAt(index);
                    FlashCards.Insert(index, flashCardCopy);
                }
                SetOperatingFlashCardCommand.Execute(new());
            }, isBusyText);
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
