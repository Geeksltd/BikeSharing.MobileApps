namespace UI.Services
{
    using System.Threading.Tasks;
    using Zebble;

    public class PushNotificationListener
    {
        internal static void Setup()
        {
            Device.PushNotification.ReceivedMessage.Handle(OnReceivedMessage);
            Device.PushNotification.Registered.Handle(OnRegistered);
            Device.PushNotification.UnRegistered.Handle(OnUnRegistered);
        }

        static async Task OnReceivedMessage(NotificationMessage message)
        {
            // TODO: Process the message.
            await Alert.Show("I received a push notification! " + message.Data);
        }

        static async Task OnRegistered(string token)
        {
            var userId = "???";

            await BaseApi.Post("v1/authentication/push-notification/register",
                new
                {
                    User = userId,
                    InstallationToken = UIRuntime.GetInstallationToken(),
                    PushNotificationToken = token,
                    DeviceType = Device.Platform
                });
        }

        static async Task OnUnRegistered()
        {
            await BaseApi.Post("v1/authentication/push-notification/unregister",
                new { InstallationToken = UIRuntime.GetInstallationToken() });
        }
    }
}