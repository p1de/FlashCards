using FlashCards.Domain.Entities.Users;
using Application = Microsoft.Maui.Controls.Application;

namespace FlashCards.Maui;

public partial class App : Application
{
	public static UserBasicInfo UserDetails;
	public static string Token;

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
