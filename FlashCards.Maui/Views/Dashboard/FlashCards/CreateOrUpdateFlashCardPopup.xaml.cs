using CommunityToolkit.Maui.Views;
using FlashCards.Domain.Entities.FlashCards;

namespace FlashCards.Maui.Views.Dashboard.FlashCards;

public partial class CreateOrUpdateFlashCardPopup : Popup
{
    FlashCard FlashCard { get; set; }

    public CreateOrUpdateFlashCardPopup()
    {
        InitializeComponent();
        submitButtonText.Text = "Create flashcard";
        FlashCard = new FlashCard();
    }

    public CreateOrUpdateFlashCardPopup(FlashCard flashCardToEdit)
    {
        InitializeComponent();
        submitButtonText.Text = "Update flashcard";
        word.Text = flashCardToEdit.Word;
        wordTranslation.Text = flashCardToEdit.WordTranslation;
        description.Text = flashCardToEdit.Description;
        FlashCard = flashCardToEdit;
    }

    private void CreateOrUpdateFlashCard(object? sender, EventArgs e)
    {
        if (FlashCard.Id != Guid.Empty.ToString())
        {
            FlashCard.Word = word.Text;
            FlashCard.WordTranslation = wordTranslation.Text;
            FlashCard.Description = description.Text;
        }
        Close(FlashCard.Id != Guid.Empty.ToString() ? FlashCard : new FlashCard()
        {
            Word = word.Text,
            WordTranslation = wordTranslation.Text,
            Description = description.Text
        });
    }
}