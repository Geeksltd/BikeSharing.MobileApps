namespace UI.Modules
{
    using Domain;
    using System.Threading.Tasks;

    partial class EventModule
    {
        private Event item;

        public Event Item { get => item; set => item = value; }

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
        }
    }
}