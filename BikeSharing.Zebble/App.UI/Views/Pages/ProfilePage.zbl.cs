namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;

    using Domain.Entities;
    using Domain.Services;
    using System.IO;
    using Domain;

    partial class ProfilePage
    {
        UserProfile Item;
        public override async Task OnInitializing()
        {
            Item = Settings.UserProfile;

            await base.OnInitializing();
            await InitializeComponents();

          
            
        }


        async Task userImageTapped()
        {
            FileInfo TempImage = new FileInfo("Images/profile_placeholder.png");
            string base64Str = null;
            try
            {

                if (Device.Permissions.Check(DevicePermission.Camera).Result == PermissionResult.Granted)              
                    TempImage = await Device.Media.TakePhoto();
                
                if(TempImage == null || TempImage.FullName.Contains("profile_placeholder"))             
                    TempImage = await Device.Media.PickPhoto();

                if (TempImage != null)
                {
                    userImageView.Path = TempImage.FullName;
                    using (Stream mediaStream = TempImage.OpenRead())
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        await mediaStream.CopyToAsync(memStream);
                        base64Str = Convert.ToBase64String(memStream.ToArray());
                    }

                    if(base64Str.HasValue())
                    {
                        var _profileService = new ProfileService();
                        await _profileService.UploadUserImageAsync( Item, base64Str);
                    }
                }
            }
            catch (Exception ex)
            {
                await Alert.Show(ex.Message);
                return;
            }
            
        }
    }
}