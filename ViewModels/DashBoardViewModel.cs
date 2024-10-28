using Boost.Common;
using Boost.Common.Classes;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boost.ViewModels
{
    public class DashBoardViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<CalenderDay> calenderDays;
        public ObservableCollection<CalenderDay> CalenderDays
        {

            get { return calenderDays; }
            set { SetProperty(ref calenderDays, value); }

        }
        private CalenderDay _selectedDay;
        public CalenderDay SelectedDay
        {
            get => _selectedDay;
            set
            {
                SetProperty(ref _selectedDay, value);
                //LoadWorkoutsForDay();
            }
        }
        #endregion


        public DashBoardViewModel()
        {
            InitData();
        }
        public async Task InitData()
        {
                //await PopulateCalenderDays();
        }
        public Command<CalenderDay> DateSelectedCommand => new Command<CalenderDay>((selectedDay) =>
        {
            SelectedDay = selectedDay;
        });
        int increment = 0;
        //public async Task PopulateCalenderDays()
        //{
        //    var currentDay = DateTime.Now;

        //    for (int i = -3; i <= 3; i++)
        //    {
        //        var day = DateTime.Now.AddDays(i);
        //        MainThread.BeginInvokeOnMainThread(async () =>
        //        {

        //            CalenderDays.Add(new CalenderDay
        //        {
        //            date = day,
        //            initial = day.ToString("MMM dd"),
        //            //Workouts = new List<Workout>()
        //        });
        //        });

        //    }

        //    SelectedDay = CalenderDays[3];
        //}
    }



}
