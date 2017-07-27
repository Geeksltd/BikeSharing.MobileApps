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

    partial class SignUpPage
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();


        }

        async Task NextButtonTapped()
        {
            var user = new UserAndProfileModel();
            user.UserName = "";
            user.Password = "";
          await   Nav.Forward<AccountPage>(new { UserAndProfileModel = user });
        }

        async Task TextChanged()
        {
            if (usernameInput.Text.HasValue() && passwordInput.Text.HasValue() && repeatPasswordInput.Text.HasValue() && repeatPasswordInput.Text == passwordInput.Text)
                nextButton.Set(rec => rec.BackgroundImagePath = "Images/floating_action_button.png");
            else
                nextButton.Set(rec => rec.BackgroundImagePath = "Images/floating_action_button_disable.png");
        }
    }
}