namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using Zebble;

    partial class LoginPage
    {
        public override async Task OnInitializing()
        {
            await SignInAsync();

            await base.OnInitializing();
            await InitializeComponents();
        }

        async Task SigninTapped() => await SignInAsync();


        async Task TextChanged()
        {
            signinButton.Enabled = usernameTextInput.Text.HasValue() && passwordTextInput.Text.HasValue();

            if (signinButton.Enabled)
                signinButton.Background(Colors.White).TextColor(Colors.Blue);
            else
                signinButton.Background(Colors.Blue).TextColor(Colors.Black);
        }
    }
}