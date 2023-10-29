namespace FlashCards.Maui.Views;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();

		if(App.UserDetails != null)
		{
			UsernameLabel.Text = App.UserDetails.Username;
			EmailLabel.Text = App.UserDetails.Email;
		}
	}
}