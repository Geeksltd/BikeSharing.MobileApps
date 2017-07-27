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

            user = Nav.Param<UserAndProfileModel>("UserAndProfileModel");
        }


        async Task NextButtonTapped()
        {
            user.FirstName = "";
            user.LastName = "";
            user.BirthDate = DateTime.Now;
            Nav.Forward<CredentialPage>(new { UserAndProfileModel = user });
        }

        async Task TextChanged()
        {
            if (firstNameInput.Text.HasValue() && lastNameInput.Text.HasValue())
                nextButton.Set(rec => rec.BackgroundImagePath = "Images/floating_action_button.png");
            else
                nextButton.Set(rec => rec.BackgroundImagePath = "Images/floating_action_button_disable.png");
        }

    }
}