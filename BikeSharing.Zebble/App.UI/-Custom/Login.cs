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
            var isValid = Validate();
            var isAuthenticated = false;

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
            var isValidUser = userName.HasValue();
            var isValidPassword = password.HasValue();
            return isValidUser && isValidPassword;
        }
    }
}
