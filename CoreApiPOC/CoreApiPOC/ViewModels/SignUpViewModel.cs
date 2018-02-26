namespace CoreApiPOC.ViewModels
{
    using CoreApiPOC.Validations;
    using CoreApiPOC.ViewModels.Base;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class SignUpViewModel : ViewModelBase
    {
        private bool _isValid;
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        private ValidatableObject<string> _userName;
        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        private ValidatableObject<string> _confirmPassword;
        public ValidatableObject<string> ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; }
        }

        public SignUpViewModel()
        {
            _userName = new ValidatableObject<string>();
            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _confirmPassword = new ValidatableObject<string>();

            AddValidations();
        }

        public ICommand MockSignUpCommand => new Command(async () => await SignUpAsync());
        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());
        public ICommand ValidateEmailCommand => new Command(() => ValidateEmail());
        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

        public ICommand EmailUnfocusedCommand => new Command(() => EmailUnfocused());
        public ICommand ConfirmPasswordUnfocusedCommand => new Command(() => ConfirmPasswordUnfocused());

        private bool ConfirmPasswordUnfocused()
        {
            _confirmPassword.Validations.Add(new IsCompareRule<string> { Text = Password.Value, ValidationMessage = "Password doesn't match." });
            return _confirmPassword.Validate();
        }

        private bool EmailUnfocused()
        {
            _email.Validations.Add(new IsEmailRule<string> { ValidationMessage = "Email in invalid format." });
            return _email.Validate();
        }

        private async Task SignUpAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();
            bool isAuthenticated = false;

            if (isValid)
            {
                try
                {
                    await Task.Delay(1000);

                    isAuthenticated = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Sign Up] Error signing up: {ex}");
                }
            }
            else
            {
                IsValid = false;
            }

            if (isAuthenticated)
            {
                await NavigationService.NavigateToAsync<LoginViewModel>();
                await NavigationService.RemoveLastFromBackStackAsync();
            }

            IsBusy = false;

        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidateEmail()
        {
            return _email.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private bool ValidateConfirmPassword()
        {
            return _confirmPassword.Validate();
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidEmail = ValidateEmail();
            bool isValidPassword = ValidatePassword();
            bool isValidConfirmPassword = ValidateConfirmPassword();
            return isValidUser && isValidPassword && isValidEmail && isValidConfirmPassword;
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username is required." });
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email is required." });
            _email.Validations.Add(new IsEmailRule<string> { ValidationMessage = "Email in invalid format." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password is required." });
            _confirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Confirm password is required." });
            
        }

    }
}
