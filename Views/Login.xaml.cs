using Boost.ViewModels;

namespace Boost.Views;

public partial class Login : ContentPage
{
    LoginViewModel vm;

    public Login()
	{
		InitializeComponent();
        vm = (LoginViewModel)BindingContext;

    }

    private void Signup_Email_Entry_Focused(object sender, FocusEventArgs e)
    {
        vm.IsSignupEmailErrorMessageVisible = false;
        
    }
    private void Signup_Username_Entry_Focused(object sender, FocusEventArgs e)
    {
        vm.IsSignupUsernameErrorMessageVisible = false;

    }

    private void Signup_Password_Entry_Focused(object sender, FocusEventArgs e)
    {
        vm.IsSignupPassswordErrorMessageVisible = false;

    }

    private void Login_Username_Entry_Focused(object sender, FocusEventArgs e)
    {
        vm.IsLoginUsernameErrorMessageVisible = false;

    }

    private void Login_Password_Entry_Focused(object sender, FocusEventArgs e)
    {
        vm.IsLoginPassswordErrorMessageVisible = false;

    }

}