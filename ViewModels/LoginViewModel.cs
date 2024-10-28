using Boost.Common;
using Boost.Views;
using Fitx_Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Boost.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command SetLoginPageLoginCommand { get; }
        public Command SetSignupPageLoginCommand { get; }
        public Command BackButtonCommand { get; }
        public Command SignupCommand { get; }
        public Command LoginCommand { get; }

        private readonly ApiService _apiservice;
        private readonly Client _client;
        public LoginViewModel()
        {
            SetLoginPageLoginCommand = new Command(SetLoginPageClicked);
            SetSignupPageLoginCommand = new Command(SetSignupPageClicked);
            BackButtonCommand = new Command(BeckButtonClicked);
            SignupCommand = new Command(async () => await SignupAsync());
            LoginCommand = new Command(async () => await LoginAsync());

            _apiservice = new ApiService();
            _client = _apiservice._apiClient;
        }

        public void SetLoginPageClicked()
        {
            IsInitialPageVisible = false;
            IsSignupPageVisible = false;
            IsLoginPageVisible = true;
        }
        public void SetSignupPageClicked()
        {
            IsInitialPageVisible = false;
            IsLoginPageVisible = false;
            IsSignupPageVisible = true;
        }
        public void BeckButtonClicked()
        {
            IsInitialPageVisible = true;
            IsLoginPageVisible = false;
            IsSignupPageVisible = false;

        }
        public bool ValidateSignupEntries()
        {
            if(!string.IsNullOrEmpty(SignupEmail)) 
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if(!Regex.IsMatch(SignupEmail, pattern))
                {
                    IsSignupEmailErrorMessageVisible = true;
                   return false;
                }
            }
            else
            {
                IsSignupEmailErrorMessageVisible = true;
                return false;
            }
            if(string.IsNullOrEmpty(SignupUsername))
            {
                IsSignupUsernameErrorMessageVisible = true;
                return false;

            }
            if (!string.IsNullOrEmpty(SignupPassword))
            {
                string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$";
                if (!Regex.IsMatch(SignupPassword, pattern))
                {
                    IsSignupPassswordErrorMessageVisible = true;
                    return false;
                }
            }
            else
            {
                IsSignupPassswordErrorMessageVisible = true;
                return false;
            }
            return true;
        }
        public bool ValidateLoginEntries()
        {
            
            if (string.IsNullOrEmpty(LoginUsername))
            {
                IsLoginUsernameErrorMessageVisible = true;
                return false;

            }
            if (string.IsNullOrEmpty(LoginPassword))
            {
                IsLoginPassswordErrorMessageVisible = true;
                return false;
            }
          
            return true;
        }

        public async Task<bool> SignupAsync()
        {

            //var test = await _client.GetUserByIDAsync(2);

            var validated = ValidateSignupEntries();
            if (validated == false)
            {
                return false;
            }
            var hashing = PasswordHasher.HashPassword(SignupPassword);
            CreateUserDTO createUserDto = new CreateUserDTO();

            createUserDto.UserName = SignupUsername;
            createUserDto.FirstName = "";
            createUserDto.LastName = "";
            createUserDto.Email = signupEmail;
            createUserDto.PasswordHash = hashing.hash;
            createUserDto.PasswordSalt = hashing.salt;
            createUserDto.CreatedBy = "System";
            createUserDto.DateCreated = DateTime.Now;
            createUserDto.LastModifiedBy = "System";
            createUserDto.DateLastModified = DateTime.Now;

            var request = await _client.CreateUserAsync(createUserDto);

            if (request.StatusCode == 200)
            {
                await Shell.Current.GoToAsync("//DashBoard");
            }

            return true;
        }

        public async Task<bool> LoginAsync()
        {
            //var validated = ValidateLoginEntries();

            //if (validated == false)
            //{
            //    return false;
            //}

            //var user = await _client.GetUserByUsernameAsync(LoginUsername);

            //var login = PasswordHasher.VerifyPassword(LoginPassword, user.PasswordHash, user.PasswordSalt);

            //if (login)
            //{
                await Shell.Current.GoToAsync("//DashBoard");
            //}
            return false;


        }
        #region SIGN UP PAGE

        private string signupUsername;
        public string SignupUsername
        {

            get { return signupUsername; }
            set { SetProperty(ref signupUsername, value); }

        }
        private string signupEmail;
        public string SignupEmail
        {

            get { return signupEmail; }
            set { SetProperty(ref signupEmail, value); }

        }
        private string signupPassword;
        public string SignupPassword
        {

            get { return signupPassword; }
            set { SetProperty(ref signupPassword, value); }

        }

        private bool isSignupEmailErrorMessageVisible = false;
        public bool IsSignupEmailErrorMessageVisible
        {

            get { return isSignupEmailErrorMessageVisible; }
            set { SetProperty(ref isSignupEmailErrorMessageVisible, value); }

        }
        private bool isSignupUsernameErrorMessageVisible = false;
        public bool IsSignupUsernameErrorMessageVisible
        {

            get { return isSignupUsernameErrorMessageVisible; }
            set { SetProperty(ref isSignupUsernameErrorMessageVisible, value); }

        }
        private bool isSignupPassswordErrorMessageVisible = false;
        public bool IsSignupPassswordErrorMessageVisible
        {

            get { return isSignupPassswordErrorMessageVisible; }
            set { SetProperty(ref isSignupPassswordErrorMessageVisible, value); }

        }
        #endregion
        #region LOGIN PAGE
        private string loginUsername;
        public string LoginUsername
        {

            get { return loginUsername; }
            set { SetProperty(ref loginUsername, value); }

        }
        private string loginPassword;
        public string LoginPassword
        {

            get { return loginPassword; }
            set { SetProperty(ref loginPassword, value); }

        }
        private bool isLoginUsernameErrorMessageVisible = false;
        public bool IsLoginUsernameErrorMessageVisible
        {

            get { return isLoginUsernameErrorMessageVisible; }
            set { SetProperty(ref isLoginUsernameErrorMessageVisible, value); }

        }
        private bool isLoginPassswordErrorMessageVisible = false;
        public bool IsLoginPassswordErrorMessageVisible
        {

            get { return isLoginPassswordErrorMessageVisible; }
            set { SetProperty(ref isLoginPassswordErrorMessageVisible, value); }

        }
        #endregion
        #region PAGE VISIBILITY
        private bool isLoginPageVisible = false;
        public bool IsLoginPageVisible
        {

            get { return isLoginPageVisible; }
            set { SetProperty(ref isLoginPageVisible, value); }

        }
        private bool isSignupPageVisible = false;
        public bool IsSignupPageVisible
        {

            get { return isSignupPageVisible; }
            set { SetProperty(ref isSignupPageVisible, value); }

        }
        private bool isInitialPageVisible = true;
        public bool IsInitialPageVisible
        {

            get { return isInitialPageVisible; }
            set { SetProperty(ref isInitialPageVisible, value); }

        }
        

        #endregion
    }
}
