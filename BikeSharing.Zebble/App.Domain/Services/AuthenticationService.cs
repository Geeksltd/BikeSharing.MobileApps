using Domain.Entities;
using System;
using System.Threading.Tasks;
using UI;
using Zebble;

namespace Domain.Services
{
    public partial class Api : BaseApi
    {
        public static class AuthenticationService
        {
            public static bool IsAuthenticated => Settings.AccessToken.HasValue();


            public static async Task<bool> LoginAsync(string userName, string password)
            {
                var auth = new AuthenticationRequest
                {
                    UserName = userName,
                    Credentials = password,
                    GrantType = "password"
                };


                string uri = $"{GlobalSettings.AuthenticationEndpoint}{"api/login"}";

                var authenticationInfo = await BaseApi.Post<AuthenticationResponse>(uri, auth);
                if (authenticationInfo == null)
                    return false;
                Settings.UserId = authenticationInfo.UserId;
                Settings.ProfileId = authenticationInfo.ProfileId;
                Settings.AccessToken = authenticationInfo.AccessToken;
                if (authenticationInfo.UserId != 0)
                {
                    Device.IO.File("Session.txt").WriteAllText(authenticationInfo.UserId.ToString());
                    return true;
                }
                return false;
                //   if (authenticationInfo.AccessToken.HasValue())
                //           Device.IO.File("SessionToken.txt").WriteAllText(authenticationInfo.AccessToken);

            }

            public static Task LogoutAsync()
            {
                Settings.RemoveUserId();
                Settings.RemoveProfileId();
                Settings.RemoveAccessToken();
                Settings.RemoveCurrentBookingId();

                return Task.CompletedTask;
            }

            public static int GetCurrentUserId() => Settings.UserId;
        }
    }
}