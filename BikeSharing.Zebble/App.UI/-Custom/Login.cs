using System;
using System.Net;
using System.Threading.Tasks;
using Zebble;
using static Domain.Services.Api;

namespace UI.Pages
{
    partial class Login
    {
        string UserName;
        string Password;

        private async Task SignInAsync()
        {
            var isValid = Validate();
            var isAuthenticated = false;

            if (isValid)
            {
                try
                {
                    isAuthenticated = await AuthenticationService.LoginAsync(UserName, Password);
                }
                catch (Exception ex) when (ex is WebException)
                {                    
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
            UserName = UsernameTextInput?.Text;
            Password = PasswordTextInput?.Text;
            var isValidUser = UserName.HasValue();
            var isValidPassword = Password.HasValue();
            return isValidUser && isValidPassword;
        }
    }
}
