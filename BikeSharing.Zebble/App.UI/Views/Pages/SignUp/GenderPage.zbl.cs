namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
     
    using Domain;
    using Domain.Entities;

    partial class GenderPage
    {
        UserAndProfileModel user;
        bool gender = false;
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

           //  user = Nav.Param<UserAndProfileModel>("UserAndProfileModel");

        }

        async Task ImageViewTapped()
        {
            if (gender)
            {
                gender = false;
                womanImageView.BackgroundImagePath = "Images/signup_woman_select.png";
                manImageView.BackgroundImagePath = "Images/signup_man.png";
            }
            else
            {
                gender = true;
                womanImageView.BackgroundImagePath = "Images/signup_woman.png";
                manImageView.BackgroundImagePath = "Images/signup_man_select.png";
            }
        }

       

        async Task NextButtonTapped()
        {
            user.Gender = gender ? "1" : "0";
           
            Nav.Forward<UserPage>(new { UserAndProfileModel = user });
        }

    }
}