using CommunityToolkit.Maui.Views;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Maui.ViewModels.FlashCards;
using FlashCards.Maui.Views.Dashboard.FlashCards;

namespace FlashCards.Maui.Pages.Dashboard;

public partial class FlashCardsPage : ContentPage
{
    FlashCardsPageViewModel _viewModel;
	public FlashCardsPage(FlashCardsPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
        _viewModel = viewModel;
	}

    protected override async void OnAppearing()
    {
        _viewModel.Page = 0;
        base.OnAppearing();
        await _viewModel.LoadFlashCardsAsync();
    }

    private async void ShowCreateOrUpdateFlashCardPopup(object sender, EventArgs e)
    {
        var popup = new CreateOrUpdateFlashCardPopup();
        if (_viewModel.OperatingFlashCard.Id != Guid.Empty.ToString())
            popup = new CreateOrUpdateFlashCardPopup(_viewModel.OperatingFlashCard);

        var result = await this.ShowPopupAsync(popup);

        if (result != null)
        {
            _viewModel.OperatingFlashCard = (FlashCard)result;
            try 
            {
                await _viewModel.CreateOrUpdateFlashCardAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        _viewModel.OperatingFlashCard = new();
    }
}