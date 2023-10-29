using FlashCards.Maui.Pages.Dashboard;
using FlashCards.Maui.ViewModels;

namespace FlashCards.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		this.BindingContext = new AppShellViewModel();

		Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
	}
}
