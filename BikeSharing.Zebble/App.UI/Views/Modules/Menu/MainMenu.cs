namespace UI.Modules
{
    using Domain;
    using Domain.Services;
    using System.Threading.Tasks;
    using UI.Pages;
    using Zebble;

    partial class MainMenu
    {

        public override async Task OnInitializing()
        {
            if (await new ProfileService().GetCurrentProfileAsync() == null)
                await Nav.Go<LoginPage>();

            await base.OnInitializing();
            await InitializeComponents();

        }


        async Task LogoutButtonTapped()
        {
            Settings.LogoutUser();
            await Nav.Go<Pages.LoginPage>();
        }

    }
}