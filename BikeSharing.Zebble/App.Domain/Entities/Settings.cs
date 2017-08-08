namespace Domain
{
    using Domain.Entities;
    using Zebble;

    public partial class Settings
    {
        /* -------------------------- Fields -------------------------*/

        private static int userId = 0;

        private static int profileId = 1;

        private static string accessToken = string.Empty;

        private static int currentBookingId = 1;

        private static UserProfile userProfile = null;


        /* -------------------------- Properties -------------------------*/


        #region Name Property

        public string Name { get; set; }
        public static int UserId { get => userId; set => userId = value; }
        public static int ProfileId { get => profileId; set => profileId = value; }
        public static string AccessToken { get => accessToken; set => accessToken = value; }
        public static int CurrentBookingId { get => currentBookingId; set => currentBookingId = value; }
        public static UserProfile UserProfile { get => userProfile; set => userProfile = value; }

        #endregion

        /* -------------------------- Methods ----------------------------*/

        public override string ToString()
        {
            return this.Name;
        }


        public static void RemoveUserId()
        {
            UserId = 0;
            Device.IO.File("Session.txt").Delete();
        }

        public static void RemoveProfileId()
        {
            ProfileId = 0;
        }

        public static void RemoveAccessToken()
        {
            AccessToken = "";
        }

        public static void RemoveCurrentBookingId()
        {
            CurrentBookingId = 0;
        }

        public static void LogoutUser()
        {
            Settings.RemoveUserId();
            Settings.RemoveProfileId();
            Settings.RemoveAccessToken();
            Settings.RemoveCurrentBookingId();

        }
    }
}