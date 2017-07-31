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
    using System.Text.RegularExpressions;

    partial class AccountPage
    {
        UserAndProfileModel user;
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

            user = Nav.Param<UserAndProfileModel>("UserAndProfileModel");
        }


        async Task NextButtonTapped()
        {        
                if (EmailValidation())
                {
                    user.Email = emailInput.Text;
                    user.Skype = skypeInput.Text;
                    await Nav.Forward<GenderPage>(new { UserAndProfileModel = user });
                }
                else
                    Alert.Toast("Email is incorrect");
        }

        async Task TextChanged()
        {
            if ( skypeInput.Text.HasValue() && EmailValidation())
                nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            else
                nextButton.Set(rec => rec.Enabled = false).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_disable.png");
        }


        bool EmailValidation()
        {
            if (emailInput.Text.HasValue())
            {
                string patternEmail = @"(?<email>\w+@\w+\.[a-z]{0,3})";
                Regex regexEmail = new Regex(patternEmail);
                if (regexEmail.IsMatch(emailInput.Text))
                    return true;
            }
            return false;
        }
    }
}