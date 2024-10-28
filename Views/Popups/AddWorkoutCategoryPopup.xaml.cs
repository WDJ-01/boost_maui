using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using Fitx_Api;
using Microsoft.Maui.Controls;

namespace Boost.Views.Popups;

public partial class AddWorkoutCategoryPopup : Popup
{
    private readonly ApiService _apiservice;
    private readonly Client _client;

    public AddWorkoutCategoryPopup()
	{
		InitializeComponent();
        _apiservice = new ApiService();
        _client = _apiservice._apiClient;

    }


}