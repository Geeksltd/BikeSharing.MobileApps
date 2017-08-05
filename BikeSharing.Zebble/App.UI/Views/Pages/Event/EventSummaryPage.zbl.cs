namespace UI.Pages
{
    using System.Threading.Tasks;


    partial class EventSummaryPage
    {

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
        }
    }
}