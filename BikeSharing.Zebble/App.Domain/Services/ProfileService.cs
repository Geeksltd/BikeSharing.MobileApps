using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;

namespace Domain.Services
{
    public class ProfileService : BaseApi
    {
        public Task<UserProfile> GetCurrentProfileAsync()
        {
            var _authenticationService = new AuthenticationService();
            var userId = _authenticationService.GetCurrentUserId();

            var builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
            builder.Path = $"api/Profiles/{userId}";

            var uri = builder.ToString();

            var result=  Api.Get<UserProfile>(uri);
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
            var builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
            builder.Path = $"api/Profiles/";
            var uri = builder.ToString();

            return Api.Post<UserAndProfileModel>(uri, profile);
        }

        public async Task UploadUserImageAsync(UserProfile profile, string imageAsBase64)
        {
            var _authenticationService = new AuthenticationService();
            var userId = _authenticationService.GetCurrentUserId();

            var builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
            builder.Path = $"api/Profiles/image/{userId}";
            var uri = builder.ToString();

            var imageModel = new ImageModel
            {
                Data = imageAsBase64
            };

            await Api.Put(uri, imageModel);
            //await CacheHelper.RemoveFromCache(profile.PhotoUrl);
        }
    }
}