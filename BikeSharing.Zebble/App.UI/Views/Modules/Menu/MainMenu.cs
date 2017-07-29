namespace UI.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Domain;
    using Zebble;
    using Domain.Services;

    partial class MainMenu
    {

        public override async Task OnInitializing()
        {
            var _profileService = new ProfileService();
            var x = await _profileService.GetCurrentProfileAsync();
           
           

            await base.OnInitializing();
            await InitializeComponents();

        }


        async Task logoutButtonTapped()
        {
            Settings.LogoutUser();

            await Nav.Go<Pages.LoginPage>();
        }

    }
}