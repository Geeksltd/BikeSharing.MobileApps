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

    partial class UsenamePage
    {
       string _userName = string.Empty;
       string _password = string.Empty;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
              
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
              
            }
        }

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
        }
        public SignUpPage signupPage => FindParent<SignUpPage>();

        async Task NextButtonTapped()
        {
            if (usernameInput.Text.HasValue() && passwordInput.Text.HasValue() && repeatPasswordInput.Text.HasValue() && passwordInput.Text == repeatPasswordInput.Text)
            {
                UserName = usernameInput.Text;
                Password = passwordInput.Text;
                await signupPage.NextPage();
            }
        }

        async Task CloseButtonTapped()
        {
          await  Nav.Go<LoginPage>();
        }
        async Task TextChanged()
        {
            if (usernameInput.Text.HasValue() && passwordInput.Text.HasValue() && repeatPasswordInput.Text.HasValue() && repeatPasswordInput.Text == passwordInput.Text)
                nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            else
                nextButton.Set(rec => rec.Enabled = false).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_disable.png");
        }
    }
}