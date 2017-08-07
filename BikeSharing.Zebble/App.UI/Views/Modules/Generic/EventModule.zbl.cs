namespace UI.Modules
{
    using System.Threading.Tasks;
    using Domain;

    partial class EventModule
    {
        public Event Item;
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
        }
    }
}