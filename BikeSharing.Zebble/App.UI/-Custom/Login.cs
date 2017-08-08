using Domain;
using System;
using System.Net;
using System.Threading.Tasks;
using Zebble;
using static Domain.Services.Api;

namespace UI.Pages
{
    partial class Login
    {
        string userName;
        string password;

        private async Task SignInAsync()
        {
            bool isValid = Validate();
            bool isAuthenticated = false;

            if (isValid)
            {
                try
                {
                    isAuthenticated = await AuthenticationService.LoginAsync(userName, password);
                }
                catch (Exception ex) when (ex is WebException)
                {
                    Console.WriteLine($"[SignIn] Error signing in: {ex}");
                    await Alert.Show("Error", "Communication error");
                }
            }

            if (isAuthenticated)
            {
                await Nav.Go<Home>();
            }
        }

        private bool Validate()
        {
            userName = UsernameTextInput?.Text;
            password = PasswordTextInput?.Text;
            bool isValidUser = userName.HasValue();
            bool isValidPassword = password.HasValue();
            return isValidUser && isValidPassword;
        }
    }
}
