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
        
        bool _gender = false;
        public bool Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;

            }
        }

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

     
        }
        public SignUpPage signupPage => FindParent<SignUpPage>();

        async Task ImageViewTapped()
        {
            nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            if (Gender)
            {
                Gender = false;
                womanImageView.Set(rec=> rec.BackgroundImagePath = "Images/SignUp/signup_woman_select.png");
                manImageView.Set(rec=> rec.BackgroundImagePath = "Images/SignUp/signup_man.png");
            }
            else
            {
                Gender = true;
                womanImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_woman.png");
                manImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_man_select.png");
            }
        }

        async Task CloseButtonTapped()
        {
            await Nav.Go<LoginPage>();
        }

        async Task NextButtonTapped()
        {

            await signupPage.NextPage();
        }

    }
}