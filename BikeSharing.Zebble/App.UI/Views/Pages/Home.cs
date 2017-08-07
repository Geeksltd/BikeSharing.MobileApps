namespace UI.Pages
{
    using System.Threading.Tasks;

    partial class Home
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
        }
    }
}