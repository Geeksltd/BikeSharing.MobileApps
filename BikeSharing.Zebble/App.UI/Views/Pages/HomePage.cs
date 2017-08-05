namespace UI.Pages
{
    using System.Threading.Tasks;

    partial class HomePage
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

        }
    }
}