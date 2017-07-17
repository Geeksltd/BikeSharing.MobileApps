namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using Zebble.Framework;
    using Zebble.Framework.Data;

    [SmallTable]
    public partial class Settings : GuidEntity
    {
        /* -------------------------- Fields -------------------------*/

        private static Settings current;

        

        private const string UserIdKey = "user_id_key";
        private static readonly int UserIdDefault = 0;

        private const string ProfileIdKey = "profile_id_key";
        private static readonly int ProfileIdDefault = 0;

        private const string AccessTokenKey = "access_token_key";
        private static readonly string AccessTokenDefault = string.Empty;

        private const string CurrentBookingIdKey = "current_booking_id";
        private static readonly int CurrentBookingIdDefault = 0;




        /* -------------------------- Properties -------------------------*/

        public static int CurrentBookingId
        {
            get
            {
                return CurrentBookingIdDefault;//CurrentBookingIdKey.LacksValue() ?  CurrentBookingIdDefault : CurrentBookingIdKey;
            }
            set
            {

            }
        }

        public static void RemoveUserId()
        {
          //  AppSettings.Remove(UserIdKey);
        }

        public static void RemoveProfileId()
        {
          //  AppSettings.Remove(ProfileIdKey);
        }

        public static void RemoveAccessToken()
        {
          //  AppSettings.Remove(AccessTokenKey);
        }

        public static void RemoveCurrentBookingId()
        {
           // AppSettings.Remove(CurrentBookingIdKey);
        }

        public static Settings Current
        {
            get
            {
                var result = current;

                if (result == null)
                {
                    current = result = Parse("Current");

                    if (result != null)
                    {
                        result.Saving += (o, e) => current = null;
                        result.Saved += (o, e) => current = null;
                        Database.CacheRefreshed += (o, e) => current = null;
                    }
                }

                return result;
            }
        }

        #region My setting 1 Property

        public int MySetting1 { get; set; }

        #endregion

        #region Name Property

        public string Name { get; set; }

        #endregion

        /* -------------------------- Methods ----------------------------*/

        public static Settings Parse(string text)
        {
            if (text.LacksValue())
            {
                throw new ArgumentNullException(nameof(text));
            }

            return Database.Find<Settings>(s => s.Name == text);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public new Settings Clone()
        {
            return (Settings)base.Clone();
        }

        protected override void ValidateProperties(ValidationResult result)
        {
            // Validate MySetting1 property:

            if (this.MySetting1 < 0)
            {
                result.Add(nameof(MySetting1), "The value of My setting 1 must be 0 or more.");
            }

            // Validate Name property:

            if (this.Name.LacksValue())
            {
                result.Add(nameof(Name), "Name cannot be empty.");
            }

            if (this.Name != null && this.Name.Length > 200)
            {
                result.Add(nameof(Name), "The provided Name is too long. A maximum of 200 characters is acceptable.");
            }
        }
    }
}