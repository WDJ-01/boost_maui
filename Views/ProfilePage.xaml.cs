using Boost.Common.Classes;
using Boost.ViewModels;
using Boost.Views.Controls;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Boost.Views.AddEditPages;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        posts.IsVisible = true;
        settings.IsVisible = false;  // Hide settings-related content
        personal.IsVisible = false;   // Hide settings-related content
        empty_post_stack.IsVisible = false;

    }

    private void OnImageTapped(object sender, EventArgs e)
    {
    }

    private async void View_TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var stack = sender as StackLayout;

        if (stack != null)
        {
            // Reset BoxView colors
            ResetBoxViewColors();

            // Set the tapped stack's BoxView color
            var tappedBox = stack.Children[1] as BoxView;
            var tappedImage = stack.Children[0] as Image;

            if (tappedBox != null && tappedImage != null)
            {
                tappedBox.BackgroundColor = Color.FromHex("#404040"); // Set color for tapped BoxView
                if (stack == posts_stack)
                {
                    tappedImage.Source = "posts_gray_dark";

                }
                else if (stack == settings_stack)
                {
                    tappedImage.Source = "settings_gray_dark";

                }
                else if (stack == personal_stack)
                {
                    tappedImage.Source = "person_gray_dark";

                }

            }

            // Toggle visibility based on which stack is tapped
            if (stack == posts_stack)
            {
                // Show posts stack-related content
                posts.IsVisible = true;
                settings.IsVisible = false;
                personal.IsVisible = false;
                personal_image.Source = "person_gray_light";
                settings_image.Source = "settings_gray_light";

            }
            else if (stack == settings_stack)
            {
                // Show settings stack-related content
                settings.IsVisible = true;
                posts.IsVisible = false;
                personal.IsVisible = false;
                personal_image.Source = "person_gray_light";
                posts_image.Source = "posts_gray_light";

            }
            else if (stack == personal_stack)
            {
                settings.IsVisible = false;
                posts.IsVisible = false;
                personal.IsVisible = true;
                settings_image.Source = "settings_gray_light";
                posts_image.Source = "posts_gray_light";

                await GetPostAsync();
            }
        }
    }

    // Helper method to reset BoxView colors
    private void ResetBoxViewColors()
    {
        var parent = posts_stack.Parent as StackLayout; // Assuming both stacks share the same parent
        if (parent != null)
        {
            foreach (var child in parent.Children)
            {
                if (child is StackLayout childStack)
                {
                    var box = childStack.Children[1] as BoxView;
                    if (box != null)
                    {
                        box.BackgroundColor = Colors.Transparent; // Reset all BoxViews to transparent
                    }
                }
            }
        }
    }

    ObservableCollection<string> postList = new ObservableCollection<string>();
    public async Task GetPostAsync()
    {
        if(posts_stack.Count == 0)
        {
            empty_post_stack.IsVisible = true;
        }

        return;

    }

    private async void Back_TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}