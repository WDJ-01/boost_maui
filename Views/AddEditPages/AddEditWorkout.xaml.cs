using Boost.Common.Classes;
using Boost.ViewModels;
using Boost.Views.Controls;
using Boost.Views.Popups;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Boost.Views.AddEditPages;

public partial class AddEditWorkout : ContentPage
{
    List<PickerItem> workoutTypePicker = new List<PickerItem>();
    List<string> workoutTypePickerNames = new List<string>();

    List<PickerItem> benchmarkWorkoutPicker = new List<PickerItem>();
    List<string> benchmarkWorkoutPickerNames = new List<string>();

    List<PickerItem> heroWorkoutPicker = new List<PickerItem>();
    List<string> heroWorkoutPickerNames = new List<string>();

    Exercise draggedItem;
    int draggedIndex = -1;

    private AddEditWorkoutViewModel model;
    public AddEditWorkout()
    {
        InitializeComponent();
        model = new AddEditWorkoutViewModel();

        addworkout_view.IsVisible = false;
        wod_picker.IsVisible = false;
        add_exercise_button.IsVisible = false;
        add_minute_button.IsVisible = false;
        emom_view.IsVisible = false;
        
        thunmbnail.IsVisible = false;

        time_switch_entry.IsVisible = false;
        rounds_switch_entry.IsVisible = false;

        PopulateWorkoutTypePicker();
        BindableLayout.SetItemsSource(workout_stack, workoutList);
        BindableLayout.SetItemsSource(emom_stack, emomList);

    }

    #region Manage workouts
    int emom_inc = 1;
    private void Add_Minute_Tapped(object sender, EventArgs e)
    {
        emomList.Add(new EmomMinute { Id = emom_inc, Name = "Minute " + emom_inc.ToString() });
        emom_inc++;
    }
    private void Remove_Minute_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button != null)
        {
            var minuteToRemove = button.BindingContext as EmomMinute;
            if (minuteToRemove != null)
            {
                emomList.Remove(minuteToRemove);

                // Update the emom_inc value and the names of all remaining items.
                for (var i = 0; i < emomList.Count; i++)
                {
                    emomList[i].Id = i + 1; // Start from 1
                    emomList[i].Name = "Minute " + (i + 1).ToString();
                }

                // Update the incrementer to reflect the current count of the list.
                emom_inc = emomList.Count + 1;

                // Refresh the BindableLayout to update the UI
                BindableLayout.SetItemsSource(emom_stack, null); // Clear the layout
                BindableLayout.SetItemsSource(emom_stack, emomList); // Rebind with the updated list
            }
        }
    }

    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        var workout = (sender as BindableObject).BindingContext as Exercise;
        if (workout != null)
        {
            draggedItem = workout;
            draggedIndex = workoutList.IndexOf(draggedItem);
        }
    }

    private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
    {
        if (draggedItem == null)
            return;

        var targetWorkout = (sender as BindableObject).BindingContext as Exercise;
        var targetIndex = workoutList.IndexOf(targetWorkout);

        // Move the dragged item
        if (draggedIndex != targetIndex && targetIndex >= 0)
        {
            workoutList.RemoveAt(draggedIndex);
            workoutList.Insert(targetIndex, draggedItem);
        }

        draggedItem = null; // Reset after drop
    }
    int inc = 0;
    private void Add_Exercise_Tapped(object sender, EventArgs e)
    {
        var popup = new AddingPopup();

        popup.OnExerciseAdded += (sender, exercises) =>
        {
            foreach(var exercise in exercises)
            {
                workoutList.Add(exercise);
            }
        };

        Shell.Current.CurrentPage.ShowPopup(popup);

        //rkoutList.Add(new PickerItem { id = 1, name = inc.ToString() });
        //c++;
    }
    private void Add_Set_Clicked(object sender, EventArgs e)
    {
        var add_button = sender as Button;

        if(add_button != null)
        {
            var exercise = add_button.BindingContext as Exercise;
            var id = exercise?.Id;
            string exercise_type = "";
            if (exercise != null)
            {
                if (exercise.IsOther)
                {
                    exercise_type = "Other";
                }
                else if(exercise.IsWeightliting)
                {
                    exercise_type = "Weightlifting";
                }
                else if (exercise.IsCalisthenics)
                {
                    exercise_type = "Calisthenics";
                }
            }
            var parent = (add_button.Parent as StackLayout)?.Parent as StackLayout;

            StackLayout target_stack;
            if (exercise.IsWeightliting)
            {
                 target_stack = (parent as StackLayout)?.Children[2] as StackLayout;
            }
            else
            {
                 target_stack = (parent as StackLayout)?.Children[2] as StackLayout;

            }

            var new_set = CreateWorkoutRow(exercise_type, (int)id, exercise.use1RM);

            target_stack?.Children.Add(new_set);
        }
    }
    private void Emom_Add_Exercise_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var parent = (button?.Parent as StackLayout).Parent as StackLayout;
        var minute = button.BindingContext as EmomMinute; // Get the specific minute to which we are adding exercises

        if (minute != null)
        {
            var popup = new AddingPopup();

            // Event handler when exercises are added from the popup
            popup.OnExerciseAdded += (popupSender, exercises) =>
            {
                foreach (var exercise in exercises)
                {
                    minute.Exercises.Add(exercise); // Add exercises to the specific minute
                }

                var binding_stack = parent.FindByName<StackLayout>("emom_exercise_stack");

                if (binding_stack != null)
                {
                    // Update UI for this minute
                    BindableLayout.SetItemsSource(binding_stack, null); // Clear previous items
                    BindableLayout.SetItemsSource(binding_stack, minute.Exercises); // Set new exercises
                }


            };

            Shell.Current.CurrentPage.ShowPopup(popup);
        }
    }


    private void Remove_Exercise_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button != null)
        {
            var workoutToRemove = button.BindingContext as Exercise;
            if (workoutToRemove != null)
            {
                workoutList.Remove(workoutToRemove);
            }
        }
    }
    private void Remove_Set_Clicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;

        if (button != null)
        {
            var childStack = button.Parent as StackLayout;

            if (childStack != null)
            {
                var parentStack = childStack.Parent as StackLayout;

                if (parentStack.Children.Contains(childStack))
                {
                    parentStack.Children.Remove(childStack);
                }
            }
        }
    }
    public StackLayout CreateWorkoutRow(string type, int id, bool IsPercentageUsed)
    {
        // Create the parent StackLayout (Horizontal)
        var horizontalStack = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Margin = new Thickness(0, 5),

        };

        // Create the "x" Label
        var label = new Label
        {
            Text = "x",
            VerticalOptions = LayoutOptions.CenterAndExpand
        };

        // Create CustomEntry for set count (Placeholder: "1")
        var setCountEntry = new CustomEntry
        {
            Placeholder = "1",
            BorderColor = Color.FromHex("#D3D3D3"), // Replace with your Gray200 color
            HeightRequest = 40,
            WidthRequest = 30,
            BorderThickness = 1,
            Padding = new Thickness(7.5),
            Margin = new Thickness(5, 0),
            VerticalOptions = LayoutOptions.CenterAndExpand
        };
        horizontalStack.Children.Add(label);
        horizontalStack.Children.Add(setCountEntry);

        if (!string.IsNullOrEmpty(type))
        {
            if(type == "Other")
            {
                var frame = new Frame
                {
                    Padding = new Thickness(5, 0),
                    WidthRequest = 50,
                    BackgroundColor = Colors.Transparent,
                    BorderColor = Color.FromHex("#D3D3D3"), // Replace with your Gray200 color
                    CornerRadius = 10
                };

                // Create the Picker inside the Frame
                var measurementPicker = new Picker
                {
                    Title = "Measurement",
                    
                };
                var list = workoutList.Where(x => x.Id == id).Select(x => x.UnitOfMeaasurements).First();
                measurementPicker.ItemsSource = list;
                measurementPicker.SelectedIndexChanged += unitOfMeasurement_SelectedIndexChanged;

                // Add Picker to the Frame
                frame.Content = measurementPicker;

                var customEntry = new CustomEntry
                {
                    Keyboard = Keyboard.Numeric,
                    Placeholder = "Enter",
                    BorderColor = Color.FromHex("#D3D3D3"), // Replace with your Gray200 color
                    PlaceholderColor = Color.FromHex("#919191"), // Light theme
                    HeightRequest = 40,
                    WidthRequest = 100,
                    BorderThickness = 1,
                    Padding = new Thickness(10),
                    Margin = new Thickness(5, 0),
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
                var unitOfMeasurementValuePicker = new Picker
                {
                    Title = "Unit",
                    IsVisible = false // Default is invisible
                };
                horizontalStack.Children.Add(frame);
                horizontalStack.Children.Add(customEntry);
                horizontalStack.Children.Add(unitOfMeasurementValuePicker);
                var removeImage = new ImageButton
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HeightRequest = 20,
                    Source = new FileImageSource
                    {
                        File = Device.RuntimePlatform == Device.iOS ? "remove_red.png" : "remove_red.png"
                    }
                };
                removeImage.Clicked += Remove_Set_Clicked;
                // Add all children to the StackLayout
                horizontalStack.Children.Add(removeImage);

            }
            else if(type == "Weightlifting")
            {
                // Create CustomEntry for weight (Placeholder: "Weight")
                var weightEntryStack = new StackLayout
                {
                    AutomationId = "1rm_stack",
                    Orientation = StackOrientation.Horizontal
                };

                var weightEntry = new CustomEntry
                {
                    Placeholder = "Weight",
                    IsVisible = IsPercentageUsed == true ? false : true,
                    BorderColor = Color.FromHex("#D3D3D3"), // Replace with your Gray200 color
                    HeightRequest = 40,
                    WidthRequest = 100,
                    BorderThickness = 1,
                    TextColor = Color.FromHex("#919191"),
                    Padding = new Thickness(10),
                    Margin = new Thickness(5, 0),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Keyboard = Keyboard.Numeric
                };
                var percentageEntry = new CustomEntry
                {
                    Placeholder = "%",
                    IsVisible = IsPercentageUsed == true ? true : false,
                    BorderColor = Color.FromHex("#D3D3D3"), // Replace with your Gray200 color
                    HeightRequest = 40,
                    WidthRequest = 40,
                    BorderThickness = 1,
                    TextColor = Color.FromHex("#919191"),
                    Padding = new Thickness(10),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Keyboard = Keyboard.Numeric
                };
                percentageEntry.TextChanged += Percentage_TextChanged;
                var percentageWeightEntry = new CustomEntry
                {
                    Placeholder = "Weight",
                    IsVisible = IsPercentageUsed == true ? true : false,
                    BorderColor = Color.FromHex("#D3D3D3"), // Replace with your Gray200 color
                    HeightRequest = 40,
                    WidthRequest = 60,
                    BorderThickness = 1,
                    TextColor = Color.FromHex("#919191"),
                    Padding = new Thickness(10),
                    Margin = new Thickness(5, 0),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Keyboard = Keyboard.Numeric
                };
                weightEntryStack.Children.Add(weightEntry);
                weightEntryStack.Children.Add(percentageEntry);
                weightEntryStack.Children.Add(percentageWeightEntry);

                // Create CustomEntry for reps (Placeholder: "Reps")
                var repsEntry = new CustomEntry
                {
                    Placeholder = "Reps",
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    BorderColor = Color.FromHex("#D3D3D3"), // Replace with your Gray200 color
                    HeightRequest = 40,
                    TextColor = Color.FromHex("#919191"),
                    WidthRequest = 50,
                    BorderThickness = 1,
                    Padding = new Thickness(10),
                    Margin = new Thickness(5, 0),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Keyboard = Keyboard.Numeric
                };
                var frame = new Frame
                {
                    Padding = new Thickness(5, 0),
                    BackgroundColor = Colors.Transparent,
                    BorderColor = Color.FromHex("#D3D3D3"), // Replace with your Gray200 color
                    CornerRadius = 10
                };
                var list = workoutList.Where(x => x.Id == id).Select(x => x.UnitOfMeaasurements).First();
                // Create a new Picker
                var picker = new Picker
                {
                    Title = "Unit"
                };
                picker.SelectedIndexChanged += unitOfMeasurement_SelectedIndexChanged;

                // Add Picker to the Frame
                frame.Content = picker;
                // Bind ItemsSource for Picker
                picker.ItemsSource = list;

                horizontalStack.Children.Add(weightEntryStack);
                horizontalStack.Children.Add(frame);
                horizontalStack.Children.Add(repsEntry);
                var removeImage = new ImageButton
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.End,
                    HeightRequest = 20,
                    Source = new FileImageSource
                    {
                        File = Device.RuntimePlatform == Device.iOS ? "remove_red.png" : "remove_red.png"
                    }
                };
                removeImage.Clicked += Remove_Set_Clicked;
                // Add all children to the StackLayout
                horizontalStack.Children.Add(removeImage);


            }
            else if (type == "Calisthenics")
            {
                var repsEntry = new CustomEntry
                {
                    Placeholder = "Reps",
                    BorderColor = Color.FromHex("#D3D3D3"), // Replace with your Gray200 color
                    HeightRequest = 40,
                    WidthRequest = 100,
                    BorderThickness = 1,
                    TextColor = Color.FromHex("#919191"),
                    Padding = new Thickness(10),
                    Margin = new Thickness(5, 0),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Keyboard = Keyboard.Numeric
                };


                horizontalStack.Children.Add(repsEntry);
                var removeImage = new ImageButton
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    HeightRequest = 20,
                    Source = new FileImageSource
                    {
                        File = Device.RuntimePlatform == Device.iOS ? "remove_red.png" : "remove_red.png"
                    }
                };
                removeImage.Clicked += Remove_Set_Clicked;
                // Add all children to the StackLayout
                horizontalStack.Children.Add(removeImage);
            }
        }

        return horizontalStack;
    }
    #endregion

    #region POPULATE
    public void PopulateWorkoutTypePicker()
    {
        workoutTypePicker.Add(new PickerItem { id = 1, name = "AMRAP" });
        workoutTypePicker.Add(new PickerItem { id = 2, name = "EMOM" });
        workoutTypePicker.Add(new PickerItem { id = 3, name = "For Time" });

        workoutTypePicker.Add(new PickerItem { id = 4, name = "Chipper" });

        workoutTypePicker.Add(new PickerItem { id = 5, name = "Ladder" });

        workoutTypePicker.Add(new PickerItem { id = 6, name = "Hero WOD" });
        workoutTypePicker.Add(new PickerItem { id = 7, name = "Benchmark Workout" });

        workoutTypePicker.Add(new PickerItem { id = 8, name = "Weightlifting" });
        workoutTypePicker.Add(new PickerItem { id = 9, name = "Custom" });
        foreach (var w in workoutTypePicker)
        {
            workoutTypePickerNames.Add(w.name);
        }

        workout_type_picker.ItemsSource = workoutTypePickerNames;
    }
    public void PopulateBenchmarkWorkoutPicker()
    {
        benchmarkWorkoutPicker.Add(new PickerItem { id = 1, name = "Angie" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 2, name = "Annie" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 3, name = "Barbara" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 4, name = "Cindy" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 5, name = "Chelsea" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 6, name = "Diane" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 7, name = "Elizabeth" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 8, name = "Fran" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 9, name = "Grace" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 10, name = "Helen" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 11, name = "Isabel" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 12, name = "Jackie" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 13, name = "Karen" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 14, name = "Kelly" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 15, name = "Linda" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 16, name = "Lynne" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 17, name = "Mary" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 18, name = "Nancy" });
        benchmarkWorkoutPicker.Add(new PickerItem { id = 19, name = "Nicole" });
        foreach (var w in benchmarkWorkoutPicker)
        {
            benchmarkWorkoutPickerNames.Add(w.name);
        }

        wod_picker.ItemsSource = benchmarkWorkoutPickerNames;
    }
    public void PopulateHeroWorkoutPicker()
    {
        heroWorkoutPicker.Add(new PickerItem { id = 1, name = "Murph" });
        heroWorkoutPicker.Add(new PickerItem { id = 2, name = "DT" });
        heroWorkoutPicker.Add(new PickerItem { id = 3, name = "Kalsu" });
        heroWorkoutPicker.Add(new PickerItem { id = 4, name = "Michael" });
        heroWorkoutPicker.Add(new PickerItem { id = 5, name = "Manion" });
        heroWorkoutPicker.Add(new PickerItem { id = 6, name = "Glen" });
        heroWorkoutPicker.Add(new PickerItem { id = 7, name = "Loredo" });
        heroWorkoutPicker.Add(new PickerItem { id = 8, name = "Josh" });
        foreach (var w in heroWorkoutPicker)
        {
            heroWorkoutPickerNames.Add(w.name);
        }
        wod_picker.ItemsSource = heroWorkoutPickerNames;

    }
    #endregion

    #region Selectionchanged
    private void workout_type_picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var workouttype = workout_type_picker.SelectedItem;

        if (workouttype != null)
        {
            switch (workouttype)
            {
                //AMRAP, EMOM, For Time
                case ("AMRAP"):
                case ("For Time"):
                    wod_picker.IsVisible = false;
                    addworkout_view.IsVisible = true;
                    add_exercise_button.IsVisible = true;
                    add_minute_button.IsVisible = false;
                    emom_view.IsVisible = false;
                    if(emomList?.Count > 0)
                    {
                        emomList.Clear();
                    }
                    emom_inc = 0;

                    break;
                case ("EMOM"):
                    wod_picker.IsVisible = false;
                    addworkout_view.IsVisible = false;
                    add_exercise_button.IsVisible = false;
                    add_minute_button.IsVisible = true;
                    emom_view.IsVisible = true;

                    break;
                //CHIPPER
                case ("Chipper"):
                    wod_picker.IsVisible = false;
                    addworkout_view.IsVisible = true;
                    add_exercise_button.IsVisible = true;
                    add_minute_button.IsVisible = false;
                    emom_view.IsVisible = false;
                    if (emomList?.Count > 0)
                    {
                        emomList.Clear();
                    }
                    emom_inc = 0;

                    break;

                //LADDER
                case ("Ladder"):
                    wod_picker.IsVisible = false;
                    addworkout_view.IsVisible = true;
                    add_minute_button.IsVisible = false;
                    add_exercise_button.IsVisible = true;
                    emom_view.IsVisible = false;
                    if (emomList?.Count > 0)
                    {
                        emomList.Clear();
                    }
                    emom_inc = 0;

                    break;

                //HERO, BENCHMARK
                case ("Hero WOD"):
                    wod_picker.IsVisible = true;
                    addworkout_view.IsVisible = false;
                    add_exercise_button.IsVisible = false;
                    add_minute_button.IsVisible = false;
                    time_switch_stack.IsVisible = false;
                    rounds_switch_entry.IsVisible = false;
                    emom_view.IsVisible = false;
                    if (emomList?.Count > 0)
                    {
                        emomList.Clear();
                    }
                    emom_inc = 0;

                    PopulateHeroWorkoutPicker();
                    break;
                case ("Benchmark Workout"):
                    wod_picker.IsVisible = true;
                    addworkout_view.IsVisible = false;
                    add_exercise_button.IsVisible = false;
                    add_minute_button.IsVisible = false;
                    emom_view.IsVisible = false;
                    time_switch_stack.IsVisible = false;
                    rounds_switch_entry.IsVisible = false;

                    if (emomList?.Count > 0)
                    {
                        emomList.Clear();
                    }
                    emom_inc = 0;

                    PopulateBenchmarkWorkoutPicker();
                    break;

                //WEIGHHTLIFTING, CUSTOM
                case ("Weightlifting"):
                case ("Custom"):
                    wod_picker.IsVisible = false;
                    addworkout_view.IsVisible = true;
                    add_exercise_button.IsVisible = true;
                    add_minute_button.IsVisible = false;
                    emom_view.IsVisible = false;
                    if (emomList?.Count > 0)
                    {
                        emomList.Clear();
                    }
                    emom_inc = 0;

                    break;
            }
        }
    }
    private void wod_picker_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    List<string> distanceUnits = new List<string> { "km", "m", "mi", "yd" };
    List<string> timeUnits = new List<string> { "hour", "min", "sec" };

    private void unitOfMeasurement_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        var parent = (picker?.Parent as Frame)?.Parent as StackLayout;
        var nextPicker = parent?.Children[4] as Picker;

        if (picker != null)
        {
            switch((string)picker.SelectedItem)
            {
                case ("Distance"):
                    if(nextPicker != null)
                    {
                        nextPicker.ItemsSource = distanceUnits;
                        nextPicker.IsVisible = true;
                    }
                    break;
                case ("Time"):
                    if (nextPicker != null)
                    {
                        nextPicker.ItemsSource = timeUnits;
                        nextPicker.IsVisible = true;

                    }
                    break;
                default:
                    break;

            }
        }
    }
    #endregion
    #region Properties
    private ObservableCollection<Exercise> workoutList = new ObservableCollection<Exercise>();
    private ObservableCollection<EmomMinute> emomList = new ObservableCollection<EmomMinute>();


    #endregion

    #region Checkboxes 

    private void use_1RM_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = sender as CheckBox;

        var sets_parent = ((checkbox?.Parent as StackLayout).Parent as StackLayout).Children[2] as StackLayout;


        if(sets_parent != null)
        {
            foreach (var child in sets_parent.Children)
            {
                // Check if the child is a StackLayout
                var target_stack = (child as StackLayout).Children.Where(x => x.AutomationId == "1rm_stack").FirstOrDefault() as StackLayout;
                if(target_stack != null)
                {
                        if (target_stack is StackLayout innerStack && innerStack.Children.Count >= 3)
                        {
                            // Access the CustomEntry elements within the nested StackLayout
                            var firstEntry = innerStack.Children[0] as CustomEntry;
                            var secondEntry = innerStack.Children[1] as CustomEntry;
                            var thirdEntry = innerStack.Children[2] as CustomEntry;

                            // Check the checkbox state and toggle visibility accordingly
                            if (e.Value) // Checkbox is checked
                            {
                                firstEntry.IsVisible = false;
                                secondEntry.IsVisible = true;
                                thirdEntry.IsVisible = true;
                            }
                            else // Checkbox is unchecked
                            {
                                firstEntry.IsVisible = true;
                                secondEntry.IsVisible = false;
                                thirdEntry.IsVisible = false;
                            }
                    }
                }

            }
        }
        //if (checkbox != null)
        //{
        //    var exercise = checkbox.BindingContext as Exercise;
        //    if (exercise != null)
        //    {
        //        exercise.use1RM = checkbox.IsChecked;
        //    }
        //}
    }


    private bool _isHandlingCheckChanged = false;

    private void Public_Visibility_CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_isHandlingCheckChanged)
            return;

        _isHandlingCheckChanged = true;

        if (e.Value) // Only change the others if the current one is checked
        {
            friends_checkbox.IsChecked = false;
            private_checkbox.IsChecked = false;
        }

        _isHandlingCheckChanged = false;
    }

    private void Friends_Visibility_CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_isHandlingCheckChanged)
            return;

        _isHandlingCheckChanged = true;

        if (e.Value)
        {
            public_checkbox.IsChecked = false;
            private_checkbox.IsChecked = false;
        }

        _isHandlingCheckChanged = false;
    }

    private void Private_Visibility_CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_isHandlingCheckChanged)
            return;

        _isHandlingCheckChanged = true;

        if (e.Value)
        {
            friends_checkbox.IsChecked = false;
            public_checkbox.IsChecked = false;
        }

        _isHandlingCheckChanged = false;
    }
    #endregion

    #region TapGestures
    private void ThumbNailTapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var popup = new AddWorkoutThumbnailPopup();

        popup.OnThumbnailAdded += (sender, source) =>
        {
            if (!string.IsNullOrEmpty(source))
            {
                thunmbnail.IsVisible = true;
                thunmbnail.Source = source;
            }
        };

        Shell.Current.CurrentPage.ShowPopup(popup);


    }
    #endregion

    #region Switches
    private void Time_Switch_Toggled(object sender, ToggledEventArgs e)
    {
        var time_switch = sender as Switch;

        if(time_switch != null)
        {
            if(time_switch.IsToggled)
            {
                time_switch_entry.IsVisible = true;
            }
            else
            {
                time_switch_entry.IsVisible = false;
            }
        }

    }

    private void Rounds_Switch_Toggled(object sender, ToggledEventArgs e)
    {
        var time_switch = sender as Switch;

        if (time_switch != null)
        {
            if (time_switch.IsToggled)
            {
                rounds_switch_entry.IsVisible = true;
            }
            else
            {
                rounds_switch_entry.IsVisible = false;
            }
        }

    }


    #endregion

    #region TextChanged
    private void Percentage_TextChanged(object sender, TextChangedEventArgs e)
    {
        int rm = 75;

        var entry = sender as CustomEntry;

        if(entry != null)
        {
            var parent = entry.Parent as StackLayout;
            var OneRMWeight = parent.Children[2] as CustomEntry;

            if (!string.IsNullOrEmpty(entry.Text))
            {
                var floatvalue = entry.Text;

                if (float.TryParse(floatvalue, out float fv))
                {
                    var value = ((float.Parse(floatvalue) / 100) * rm).ToString();
                    if (OneRMWeight != null)
                    {
                        OneRMWeight.Text = value != null ? value : "0";
                    }

                }

            }
        }

    }
    #endregion
}