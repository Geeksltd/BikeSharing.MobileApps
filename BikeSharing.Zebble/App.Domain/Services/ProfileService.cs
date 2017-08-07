using System;
using System.Threading.Tasks;
using Domain.Entities;
using UI;
using Zebble;

namespace Domain.Services
{
    public class ProfileService : BaseApi
    {
        public async Task<UserProfile> GetCurrentProfileAsync()
        {
            var userId = new AuthenticationService().GetCurrentUserId();
            var builder = new UriBuilder(string.Format("{0}api/Profiles/{1}", GlobalSettings.AuthenticationEndpoint, userId));
            var uri = builder.ToString();
            var result = await Get<UserProfile>(uri);
            if (result != null)
            {
                //   result.Result.PhotoUrl = string.IsNullOrEmpty(result.Result.PhotoUrl) ? "Images/profile_placeholder.png" : result.Result.PhotoUrl;
                result.PhotoUrl = "Images/profile_placeholder.png";
                Settings.UserProfile = result;
            }
            return result;
        }

        public Task<UserAndProfileModel> SignUp(UserAndProfileModel profile)
        {
            var uri = string.Format("{0}api/Profiles/", GlobalSettings.AuthenticationEndpoint);
            return Post<UserAndProfileModel>(uri, profile);
        }

        public Task UploadUserImageAsync(string imageAsBase64, UserProfile profile)
        {
            var uri = string.Format("{0}api/Profiles/image/{1}", GlobalSettings.AuthenticationEndpoint, profile.UserId);

            return Put(uri, new { Data = imageAsBase64 });
        }
    }
}