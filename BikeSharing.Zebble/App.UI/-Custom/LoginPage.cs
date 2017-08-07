using Domain;
using Domain.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using Zebble;
using static Domain.Services.Api;

namespace UI.Pages
{
    partial class Login
    {
        private string UserName;
        private string Password;
        private bool IsValid;

        private async Task SignInAsync()
        {
            IsValid = true;
            bool isValid = Validate();
            bool isAuthenticated = false;

            if (isValid)
            {
                try
                {
                    isAuthenticated = await AuthenticationService.LoginAsync(UserName, Password);
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
                await Nav.Go<Home>();
            }
        }

        private bool Validate()
        {
            UserName = usernameTextInput?.Text;
            Password = passwordTextInput?.Text;
            bool isValidUser = UserName.HasValue();
            bool isValidPassword = Password.HasValue();
            return isValidUser && isValidPassword;
        }
    }

}
