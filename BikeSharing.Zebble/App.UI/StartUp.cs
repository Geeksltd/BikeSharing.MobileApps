namespace UI
{
    using System;
    using System.Threading.Tasks;
    using Zebble;
    using Zebble.Services;
    using static Domain.Services.Api;

    public partial class StartUp : Zebble.StartUp
    {
        public override async Task Run()
        {
            ApplicationName = "BikeSharing";

            await InstallIfNeeded();

            //  RegisterDataProvider(typeof(AppData.AdoDotNetDataProviderFactory));

            CssStyles.LoadAll();
            ImageService.MemoryCacheFolder("Images");

            //Device.System.ReceivedMemoryWarning.Handle(() => Alert.Toast("There is a shortage of memory. The application may crash."));

            Services.PushNotificationListener.Setup();

            LoadFirstPageAsync().RunInParallel();
        }

        public static async Task LoadFirstPageAsync()
        {
            if (await ProfileService.GetCurrentProfileAsync() == null)
                await Nav.Go<Pages.Login>();
            else
                await Nav.Go(new Pages.Home());
        }
    }
}