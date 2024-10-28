using Boost.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using Boost.Views.Popups;
using System.Collections.ObjectModel;
using Boost.Common.Classes;

namespace Boost.ViewModels
{
    public class WorkoutsViewModel : BaseViewModel
    {
        public Command AddButtonCommand { get; }

        public WorkoutsViewModel() 
        {
            AddButtonCommand = new Command(AddButton);
            Tabs = new ObservableCollection<TabItem>();
            WorkoutsList = new ObservableCollection<Workout>();

            PopulateTabs();

        }

        public void AddButton()
        {
            //Shell.Current.CurrentPage.ShowPopup(AddingPopup);
            Shell.Current.GoToAsync("AddEditWorkout");
        }

        public async Task PopulateTabs()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Tabs.Add(new TabItem { Name = "My Workouts", BackgroundColor = Color.FromHex("#DC5C00"),Id = 1 });
                Tabs.Add(new TabItem { Name = "History", BackgroundColor = Color.FromHex("#919191"), Id = 2 });
                Tabs.Add(new TabItem { Name = "Favorites", BackgroundColor = Color.FromHex("#919191"), Id = 3 });


            });
            MainThread.BeginInvokeOnMainThread(() =>
            {
                WorkoutsList.Add(new Workout { Name = "Back & Biceps", Description = "Tricep Extension, Bench Press, Tricep Extension, Pec Fly, Dips,Tricep Extension, Bench Press, Tricep Extension, Pec Fly, Dips  ", Id = 1 });
                WorkoutsList.Add(new Workout { Name = "Back & Biceps", Description = "Tricep Extension, Bench Press, Tricep Extension, Pec Fly, Dips,Tricep Extension, Bench Press, Tricep Extension, Pec Fly, Dips  ", Id = 1 });
                WorkoutsList.Add(new Workout { Name = "Back & Biceps", Description = "Tricep Extension, Bench Press, Tricep Extension, Pec Fly, Dips,Tricep Extension, Bench Press, Tricep Extension, Pec Fly, Dips  ", Id = 1 });
                WorkoutsList.Add(new Workout { Name = "Back & Biceps", Description = "Tricep Extension, Bench Press, Tricep Extension, Pec Fly, Dips,Tricep Extension, Bench Press, Tricep Extension, Pec Fly, Dips  ", Id = 1 });

            });

        }


        #region Lists

        private ObservableCollection<TabItem> tabs ;
        public ObservableCollection<TabItem> Tabs
        {
            get { return tabs; }
            set { SetProperty(ref tabs, value); }
        }

        private ObservableCollection<Workout> workoutsList;
        public ObservableCollection<Workout> WorkoutsList
        {
            get { return workoutsList; }
            set { SetProperty(ref workoutsList, value); }
        }
        #endregion

    }
}
