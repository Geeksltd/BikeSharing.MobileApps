using Domain.Entities;
using System;
using System.Threading.Tasks;
using UI;
using Zebble;

namespace Domain.Services
{
    public partial class Api : BaseApi
    {
        public static class ProfileService
        {
            public static async Task<UserProfile> GetCurrentProfileAsync()
            {
                var userId = AuthenticationService.GetCurrentUserId();

                var uri = $"{GlobalSettings.AuthenticationEndpoint}api/Profiles/{userId}";
                var result = await BaseApi.Get<UserProfile>(uri, errorAction: OnError.Ignore);
                if (result != null)
                {
                    //   result.Result.PhotoUrl = string.IsNullOrEmpty(result.Result.PhotoUrl) ? "Images/profile_placeholder.png" : result.Result.PhotoUrl;
                    result.PhotoUrl = "Images/profile_placeholder.png";
                    Settings.UserProfile = result;
                }
                return result;
            }

            public static async Task<UserAndProfileModel> SignUpAsync(UserAndProfileModel profile)
            {
                var uri = $"{GlobalSettings.AuthenticationEndpoint}api/Profiles/";
                return await BaseApi.Post<UserAndProfileModel>(uri, profile);
            }

            public static async Task UploadUserImageAsync(string imageAsBase64, UserProfile profile)
            {
                var uri = $"{GlobalSettings.AuthenticationEndpoint}api/Profiles/image/{profile.UserId}";

                var imageModel = new ImageModel
                {
                    Data = imageAsBase64
                };

                await BaseApi.Put(uri, imageModel);
                //await CacheHelper.RemoveFromCache(profile.PhotoUrl);
            }
        }
    }
}