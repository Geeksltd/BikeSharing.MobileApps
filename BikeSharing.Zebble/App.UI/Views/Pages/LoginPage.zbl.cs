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
            if (usernameTextInput.Text.HasValue() && passwordTextInput.Text.HasValue())
                signinButton.Set(x => x.Style.BackgroundColor = Colors.White).Set(rec => rec.Style.TextColor = Colors.Blue).Set(rec => rec.Enabled = true);
            else
                signinButton.Set(x => x.Style.BackgroundColor = Colors.Blue).Set(rec => rec.Style.TextColor = Colors.Black).Set(rec => rec.Enabled = false);
        }



    }
}