namespace Boost.Views.Controls;

public partial class TopNav : ContentView
{
	public TopNav()
	{
		InitializeComponent();
	}

    private void OnImage_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("ProfilePage");
    }
}