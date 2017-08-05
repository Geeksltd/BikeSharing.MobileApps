using Domain.Entities;
using System;
using System.Threading.Tasks;
using UI;
using Zebble;

namespace Domain.Services
{
    public class ProfileService : BaseApi
    {
        public Task<UserProfile> GetCurrentProfileAsync()
        {
            var userId = new AuthenticationService().GetCurrentUserId();
            var builder = new UriBuilder(string.Format("{0}api/Profiles/{1}", GlobalSettings.AuthenticationEndpoint, userId));
            var uri = builder.ToString();
            var result = BaseApi.Get<UserProfile>(uri);
            if (result.Result != null)
            {
                //   result.Result.PhotoUrl = string.IsNullOrEmpty(result.Result.PhotoUrl) ? "Images/profile_placeholder.png" : result.Result.PhotoUrl;
                result.Result.PhotoUrl = "Images/profile_placeholder.png";
                Settings.UserProfile = result.Result;
            }
            return result;
        }

        public Task<UserAndProfileModel> SignUp(UserAndProfileModel profile)
        {
            var builder = new UriBuilder(string.Format("{0}api/Profiles/", GlobalSettings.AuthenticationEndpoint));
            var uri = builder.ToString();
            return BaseApi.Post<UserAndProfileModel>(uri, profile);
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