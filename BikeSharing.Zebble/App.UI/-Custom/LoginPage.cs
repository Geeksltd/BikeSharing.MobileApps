using System;
using System.Net;
using System.Threading.Tasks;
using Domain;
using Domain.Services;
using Zebble;

namespace UI.Pages
{
    partial class LoginPage
    {
        string _userName;
        string _password;
        bool _isValid;


        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
            }
        }

        async Task SignInAsync()
        {
            IsValid = true;
            bool isValid = Validate();
            bool isAuthenticated = false;

            if (isValid)
            {
                try
                {
                    isAuthenticated = await new AuthenticationService().LoginAsync(UserName, Password);
                }
                catch (Exception ex) when (ex is WebException)
                {
                    Console.WriteLine($"[SignIn] Error signing in: {ex}");
                    await Alert.Show("Error", "Communication error");
                }
            }
            else
            {
                var file = Device.IO.File("Session.txt");
                if (file.Exists())
                {
                    var data = file.ReadAllText();
                    Settings.UserId = Convert.ToInt32(data);
                    if (Settings.UserId != 0)
                        isAuthenticated = true;
                }


                IsValid = false;
            }

            if (isAuthenticated)
            {
                await Nav.Go<HomePage>();
            }
        }

        bool Validate()
        {
            _userName = usernameTextInput?.Text;
            _password = passwordTextInput?.Text;
            bool isValidUser = _userName.HasValue();
            bool isValidPassword = _password.HasValue();
            return isValidUser && isValidPassword;
        }
    }

}
