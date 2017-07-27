namespace Domain
{
    using System;
    using Zebble;
    using Zebble.Data;
   

    /// <summary>
    /// Provides the business logic for Settings class.
    /// </summary>
    partial class Settings
    {
        /// <summary>
        /// Validates this instance to ensure it can be saved in a data repository.
        /// If this finds an issue, it throws a ValidationException for that.        
        /// This calls ValidateProperties(). Override this method to provide custom validation logic in a type.
        /// </summary>
        public override void Validate(ValidationResult result)
        {
            base.Validate(result);

            if (IsNew && Database.Any<Settings>())
                throw new Exception("Settings is Singleton!");
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