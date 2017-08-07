namespace UI.Modules
{
    using System.Threading.Tasks;
    using Domain;

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