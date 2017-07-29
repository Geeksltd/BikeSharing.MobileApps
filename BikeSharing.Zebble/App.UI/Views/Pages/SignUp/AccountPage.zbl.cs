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
            if (emailInput.Text.HasValue() )
            {
                string patternEmail = @"(?<email>\w+@\w+\.[a-z]{0,3})";
                Regex regexEmail = new Regex(patternEmail);
                if (regexEmail.IsMatch(emailInput.Text))
                {
                    user.Email = emailInput.Text;
                    user.Skype = skypeInput.Text;
                    await Nav.Forward<GenderPage>(new { UserAndProfileModel = user });
                }
                else
                    Alert.Toast("Email is incorrect");
            }
        }

        async Task TextChanged()
        {
            if (emailInput.Text.HasValue() && skypeInput.Text.HasValue() )
                nextButton.Set(rec => rec.BackgroundImagePath = "Images/floating_action_button.png");
            else
                nextButton.Set(rec => rec.BackgroundImagePath = "Images/floating_action_button_disable.png");
        }

    }
}