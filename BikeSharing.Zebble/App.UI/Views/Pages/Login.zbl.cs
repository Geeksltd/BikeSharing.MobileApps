namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using Zebble;

    partial class Login
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
          
        }

        async Task SigninTapped() => await SignInAsync();


        async Task TextChanged()
        {
            SigninButton.Enabled = UsernameTextInput.Text.HasValue() && PasswordTextInput.Text.HasValue();

            if (SigninButton.Enabled)
                SigninButton.Background(Colors.White).TextColor(Colors.Blue);
            else
                SigninButton.Background(Colors.Blue).TextColor(Colors.Black);
        }
    }
}