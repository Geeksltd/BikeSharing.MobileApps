using Domain.Entities;
using System;
using System.Threading.Tasks;
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
            var result = await BaseApi.Get<UserProfile>(uri, errorAction: OnError.Ignore);
            if (result != null)
            {
                //   result.Result.PhotoUrl = string.IsNullOrEmpty(result.Result.PhotoUrl) ? "Images/profile_placeholder.png" : result.Result.PhotoUrl;
                result.PhotoUrl = "Images/profile_placeholder.png";
                Settings.UserProfile = result;
            }
            return result;
        }

        public async Task<UserAndProfileModel> SignUpAsync(UserAndProfileModel profile)
        {
            var builder = new UriBuilder(string.Format("{0}api/Profiles/", GlobalSettings.AuthenticationEndpoint));
            var uri = builder.ToString();
            return await BaseApi.Post<UserAndProfileModel>(uri, profile);
        }

        public async Task UploadUserImageAsync(string imageAsBase64, UserProfile profile)
        {
            var builder = new UriBuilder(string.Format("{0}api/Profiles/image/{1}", GlobalSettings.AuthenticationEndpoint, profile.UserId));
            var uri = builder.ToString();

            var imageModel = new ImageModel
            {
                Data = imageAsBase64
            };

            await BaseApi.Put(uri, imageModel);
            //await CacheHelper.RemoveFromCache(profile.PhotoUrl);
        }
    }
}