using Boost.Common.Classes;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using Fitx_Api;
using Microsoft.Maui.Controls;

namespace Boost.Views.Popups;

public partial class AddingPopup : Popup
{
    private readonly ApiService _apiservice;
    private readonly Client _client;
    public event EventHandler<List<Exercise>> OnExerciseAdded;
    public AddingPopup()
	{
		InitializeComponent();
        _apiservice = new ApiService();
        _client = _apiservice._apiClient;

        Populate_Popup();

    }
    public List<Exercise> exercise_list = new List<Exercise>();
    public async void Populate_Popup()
    {
        activity_indicator.IsRunning = true;
        activity_indicator.IsVisible = true;
        exercise_list.Add(new Exercise
        {
            Id = 1,
            Name = "Bench Press",
            UnitOfMeaasurements = new List<string> { "kg", "lbs", "g", "stone" },
            IsWeightliting = true
            
            
        });
        exercise_list.Add(new Exercise
        {
            Id = 2,
            Name = "Calf Stretch",
            UnitOfMeaasurements = new List<string> { "Time" },
            IsOther = true

        });

        exercise_list.Add(new Exercise
        {
            Id = 4,
            Name = "PVC Passthroughs",
            UnitOfMeaasurements = new List<string> { "Reps", "Time" },
            IsOther = true

        });
        exercise_list.Add(new Exercise
        {
            Id = 5,
            Name = "Run",
            UnitOfMeaasurements = new List<string> { "Distance", "Time" },
            IsOther = true

        }); 
        exercise_list.Add(new Exercise
        {
            Id = 6,
            Name = "Assault Bike",
            UnitOfMeaasurements = new List<string> { "Distance", "Time", "Calories" },
            IsOther = true

        });
        exercise_list.Add(new Exercise
        {
            Id = 7,
            Name = "Wall Balls",
            UnitOfMeaasurements = new List<string> { "Reps", "Time" },
            IsOther= true

        });
        exercise_list.Add(new Exercise
        {
            Id = 8,
            Name = "Pull Ups",
            UnitOfMeaasurements = new List<string> { "Reps" },
            IsCalisthenics = true

        });
        exercise_list.Add(new Exercise
        {
            Id = 9,
            Name = "Rest",
            UnitOfMeaasurements = new List<string> { "Time" },
            IsRest = true

        });
        //r exercises = await _client.GetAllExercisesAsync();
        collectionView.ItemsSource = exercise_list.GroupBy(x => x.Name.Substring(0,1)).OrderBy(x => x.Key);

        activity_indicator.IsRunning = false;
        activity_indicator.IsVisible = false;

    }

    private void Add_Button_Clicked(object sender, EventArgs e)
    {
        var selected = new List<Exercise>();

        foreach(var exercise in collectionView.SelectedItems)
        {
            var item = exercise as Exercise;
            if(item != null)
            {
                selected.Add(item);
            }
        }
        OnExerciseAdded?.Invoke(this, selected);

        Close();
    }

    private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = collectionView.SelectedItems;

        int count = selected.Count();

        add_btn.Text = $"Add Exercises ({count})";
    }
}