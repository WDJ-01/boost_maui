using System.Windows.Input;

namespace Boost.Views.Controls;

public partial class SecondaryTopNav : ContentView
{


    public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(AddEditTopNav), default(string));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }


    public static readonly BindableProperty BackTapCommandProperty =
           BindableProperty.Create(nameof(BackTapCommand), typeof(ICommand), typeof(AddEditTopNav));

    public ICommand BackTapCommand
    {
        get { return (ICommand)GetValue(BackTapCommandProperty); }
        set { SetValue(BackTapCommandProperty, value); }
    }

    public SecondaryTopNav()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}