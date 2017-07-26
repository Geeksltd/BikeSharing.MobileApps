namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
     
    using Domain;

    partial class LoginPage
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

             SignInAsync();
        }

        async Task signinTapped()
        {
             SignInAsync();
        }

        async Task TextChanged() {
            if( usernameTextInput.Text.HasValue() && passwordTextInput.Text.HasValue() )
              signinButton.Set(x => x.Style.BackgroundColor = Colors.White).Set(rec=> rec.Style.TextColor = Colors.Blue).Set(rec=> rec.Enabled=true);
            else
                signinButton.Set(x => x.Style.BackgroundColor = Colors.Blue).Set(rec => rec.Style.TextColor = Colors.Black).Set(rec => rec.Enabled = false);
        }

       
        
    }
}