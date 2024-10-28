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

namespace Boost.ViewModels
{
    public class AddEditWorkoutViewModel : BaseViewModel
    {
        public Command BackButtonCommand { get; set; }
        public Command AddExerciseCommand { get; set; }

        public AddEditWorkoutViewModel() 
        {
            BackButtonCommand = new Command(BackButton);
            AddExerciseCommand = new Command(AddExerciseButton);
        }

        public async void BackButton()
        {
            //Shell.Current.CurrentPage.ShowPopup(AddingPopup);
            await Shell.Current.Navigation.PopAsync();
        }
        public void AddExerciseButton()
        {
            Shell.Current.CurrentPage.ShowPopup(new AddingPopup());
        }

        #region Properties
        private ObservableCollection<string> workoutList;
        public ObservableCollection<string> WorkoutList
        {

            get { return workoutList; }
            set { SetProperty(ref workoutList, value); }

        }
        #endregion

    }
}
