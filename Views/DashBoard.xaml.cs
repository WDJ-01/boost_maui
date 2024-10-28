using Boost.Common.Classes;
using Boost.ViewModels;
using Microsoft.Maui.ApplicationModel;
using System.Collections.ObjectModel;

namespace Boost.Views;

public partial class DashBoard : ContentPage
{
    private DashBoardViewModel model;
    public ObservableCollection<CalenderDay> CalenderDays { get; set; }

public DashBoard()
	{

		InitializeComponent();
        model = new DashBoardViewModel();
        PopulateCalenderDays();
    }

    //private void CalendarView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    var selectedItem = e.CurrentSelection.FirstOrDefault() as CalenderDay;
    //    if (selectedItem != null)
    //    {
    //        foreach (var day in CalenderDays)
    //        {
    //            if (day == selectedItem)
    //            {
    //                day.selectedTextColor = Colors.White;
    //                day.selectedColor = Color.FromHex("#DC5C00");
    //            }
    //            else
    //            {
    //                day.selectedTextColor = Color.FromHex("#919191");
    //                day.selectedColor = Colors.White;
    //            }
    //        }
    //    }
    //}

    public async Task PopulateCalenderDays()
    {
        CalenderDays = new ObservableCollection<CalenderDay>();

        for (int i = -3; i <= 31; i++)
        {
            var day = DateTime.Now.AddDays(i);
            CalenderDays.Add(new CalenderDay
            {
                date = day,
                dayDate = day.Day,
                initial = day.DayOfWeek.ToString().Substring(0, 1),
                selectedColor = i == 0 ? Color.FromHex("#DC5C00") : Application.Current.RequestedTheme == AppTheme.Dark ? Color.FromHex("#252525") : Colors.White,
                selectedTextColor = i == 0 ? Colors.White : Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Color.FromHex("#919191")
            }); ;

        }
        BindableLayout.SetItemsSource(CalendarView, CalenderDays);
       // CalendarView.ItemSource = CalenderDays;
        //CalendarView.SelectedItem = CalenderDays[3];
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
       // var selectedItem = sender as CalenderDay;
        var selectedItem = (sender as BindableObject).BindingContext as CalenderDay;

        if (selectedItem != null)
        {
            foreach (var day in CalenderDays)
            {
                if (day == selectedItem)
                {
                    day.selectedTextColor = Colors.White;
                    day.selectedColor = Color.FromHex("#DC5C00");
                }
                else
                {
                    day.selectedTextColor =  Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Color.FromHex("#919191");
                    day.selectedColor = Application.Current.RequestedTheme == AppTheme.Dark ? Color.FromHex("#252525") : Colors.White;
                }
            }
        }
    }
}