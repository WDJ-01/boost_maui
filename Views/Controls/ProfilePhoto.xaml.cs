using System.Windows.Input;

namespace Boost.Views.Controls;

public partial class ProfilePhoto : ContentView
{



    public ProfilePhoto()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty SourceImageProperty = BindableProperty.Create(
            nameof(SourceImage), typeof(string), typeof(ProfilePhoto), string.Empty);

    public string SourceImage
    {
        get => (string)GetValue(SourceImageProperty);
        set => SetValue(SourceImageProperty, value);
    }

    // Event that allows external handling of the tap
    public event EventHandler ImageTapped;

    // Tap handler
    private void OnImageTapped(object sender, EventArgs e)
    {
        // Raise the custom ImageTapped event
        ImageTapped?.Invoke(this, EventArgs.Empty);
    }


}