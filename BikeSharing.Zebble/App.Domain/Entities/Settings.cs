namespace Domain
{
    using Domain.Entities;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using Zebble.Data;

    [SmallTable]
    public partial class Settings : GuidEntity
    {
        /* -------------------------- Fields -------------------------*/

        private static Settings current;

        

       
        private static  int UserIdKey = 1;

      
        private static  int ProfileIdKey = 1;

    
        private static  string AccessTokenKey = string.Empty;

      
        private static  int CurrentBookingIdKey = 1;

        private static UserProfile _userProfile = null;


        /* -------------------------- Properties -------------------------*/

        public static UserProfile UserProfile
        {
            get
            {
                return _userProfile;
            }
            set
            {
                _userProfile = value;
            }
        }

        public static int ProfileId
        {
            get
            {
                return ProfileIdKey;
            }
            set
            {
                ProfileIdKey= value;
            }
        }
        public static string AccessToken
        {
            get
            {
                return AccessTokenKey;
            }
            set
            {
                AccessTokenKey= value;
            }
        }

        public static int CurrentBookingId
        {
            get
            {
                return  CurrentBookingIdKey;
            }
            set
            {
                CurrentBookingIdKey = value;
            }
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

        #region UserId Property

        public static int UserId
        {
            get
            {
                return UserIdKey;
            }
            set
            {
                UserIdKey = value;
            }
        }

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