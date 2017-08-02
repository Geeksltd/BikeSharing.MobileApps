using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;

namespace Domain.Services
{
    public class AuthenticationService : Api
    {
        public bool IsAuthenticated => Settings.AccessToken.HasValue();

      
        public async Task<bool> LoginAsync(string userName, string password)
        {
            var auth = new AuthenticationRequest
            {
                UserName = userName,
                Credentials = password,
                GrantType = "password"
            };

            var builder = new UriBuilder(string.Format("{0}{1}", GlobalSettings.AuthenticationEndpoint, "api/login"));
            string uri = builder.ToString();

            var authenticationInfo = await Api.Post<AuthenticationResponse>(uri, auth);
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

        public Task LogoutAsync()
        {
            Settings.RemoveUserId();
            Settings.RemoveProfileId();
            Settings.RemoveAccessToken();
            Settings.RemoveCurrentBookingId();

            return Task.CompletedTask;
        }

        public int GetCurrentUserId() => Settings.UserId;
    }
}
