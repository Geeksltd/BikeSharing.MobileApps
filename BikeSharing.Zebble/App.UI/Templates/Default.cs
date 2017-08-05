namespace UI.Templates
{
    using System.Threading.Tasks;
    using Zebble;

    public class Default : NavBarPage
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            //  BodyScroller.Background(Colors.White);
            await WhenShown(() => new MenuDisplayer().Setup());
        }



        protected override View CreateBackButton() => new IconButton { CssClass = "navbar-button back" };

        protected override View CreateMenuIcon() => new ImageView { CssClass = "menu-icon" };

        protected override Task OnMenuTapped()
        {
            MenuDisplayer.Current.Y.Set(this.GetNavBar().ActualHeight);
            MenuDisplayer.Current.Height.Set(View.Root.ActualHeight - MenuDisplayer.Current.Y.CurrentValue);

            MenuDisplayer.Current.Show();
            return Task.CompletedTask;
        }
    }
}