using Boost.ViewModels;
using Boost.Views.Popups;
using CommunityToolkit.Maui.Views;
using System.Security.Cryptography.X509Certificates;

namespace Boost.Views;

public partial class Workouts : ContentPage
{

    WorkoutsViewModel vm;
	public Workouts()
	{
		InitializeComponent();
        vm = (WorkoutsViewModel)BindingContext;
    }

    private void Add_Category_Clicked(object sender, EventArgs e)
    {
       Shell.Current.CurrentPage.ShowPopup(new AddWorkoutCategoryPopup());
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("ActiveWorkoutPage");
    }
}