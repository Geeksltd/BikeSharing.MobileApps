namespace UI.Modules
{
    using Domain;
    using System.Threading.Tasks;

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