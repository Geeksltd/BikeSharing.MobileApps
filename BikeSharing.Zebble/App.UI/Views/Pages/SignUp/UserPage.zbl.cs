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

    partial class UserPage
    {
        UserAndProfileModel user;
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

            birthdateInput.SelectedValue = new DateTime(1970, 01, 01);
            user = Nav.Param<UserAndProfileModel>("UserAndProfileModel");
        }


        async Task NextButtonTapped()
        {
            user.FirstName = firstNameInput.Text;
            user.LastName = lastNameInput.Text;
            user.BirthDate = birthdateInput.SelectedValue.Value;
            Nav.Forward<SubscriptionPage>(new { UserAndProfileModel = user });
        }

        async Task TextChanged()
        {
            if (firstNameInput.Text.HasValue() && lastNameInput.Text.HasValue())
                nextButton.Set(rec=> rec.Enabled= true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            else
                nextButton.Set(rec => rec.Enabled = false).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_disable.png");
        }

    }
}