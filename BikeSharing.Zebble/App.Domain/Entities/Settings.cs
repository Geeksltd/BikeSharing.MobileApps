namespace Domain
{
    using Domain.Entities;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using Zebble;
  
  
    public partial class Settings
    {
        /* -------------------------- Fields -------------------------*/

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