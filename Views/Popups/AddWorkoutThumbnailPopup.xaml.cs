using Boost.Common.Classes;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using Fitx_Api;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace Boost.Views.Popups;

public partial class AddWorkoutThumbnailPopup : Popup
{
    public event EventHandler<string> OnThumbnailAdded;
    public ObservableCollection<string> Thumbnails = new ObservableCollection<string>();
    public AddWorkoutThumbnailPopup()
	{
		InitializeComponent();
        Populate_Popup();

    }
    public async void Populate_Popup()
    {
        activity_indicator.IsRunning = true;
        activity_indicator.IsVisible = true;

        Thumbnails.Add("temp_1.jpg");

        activity_indicator.IsRunning = false;
        activity_indicator.IsVisible = false;

    }

    private void Add_Button_Clicked(object sender, EventArgs e)
    {
        var selected = (string)collectionView.SelectedItem;

        OnThumbnailAdded?.Invoke(this, selected);

        Close();
    }
}