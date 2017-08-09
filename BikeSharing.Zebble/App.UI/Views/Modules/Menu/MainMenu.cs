namespace UI.Modules
{
    using Domain;
    using System.Threading.Tasks;
    using UI.Pages;
    using Zebble;

    partial class MainMenu
    {
        public override async Task OnInitializing()
        {
            if (Settings.UserProfile == null)
            {
                await Nav.Go<Login>();
                return;
            }
            await base.OnInitializing();
            await InitializeComponents();
        }

        async Task LogoutButtonTapped()
        {
            Settings.LogoutUser();
            await Nav.Go<Pages.Login>();
        }
    }
}