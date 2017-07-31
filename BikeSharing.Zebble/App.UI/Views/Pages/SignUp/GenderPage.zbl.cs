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

            user = Nav.Param<UserAndProfileModel>("UserAndProfileModel");

        }

        async Task ImageViewTapped()
        {
            nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            if (gender)
            {
                gender = false;
                womanImageView.Set(rec=> rec.BackgroundImagePath = "Images/SignUp/signup_woman_select.png");
                manImageView.Set(rec=> rec.BackgroundImagePath = "Images/SignUp/signup_man.png");
            }
            else
            {
                gender = true;
                womanImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_woman.png");
                manImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_man_select.png");
            }
        }

       

        async Task NextButtonTapped()
        {
            user.Gender = gender ? "Female" : "Male";

            await Nav.Forward<UserPage>(new { UserAndProfileModel = user });
        }

    }
}