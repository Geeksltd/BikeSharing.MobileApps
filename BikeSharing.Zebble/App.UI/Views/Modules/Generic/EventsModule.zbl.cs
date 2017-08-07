namespace UI.Modules
{
    using System;
    using System.Threading.Tasks;
    using Domain;
    using UI;
    using UI.Pages;
    using Zebble;

    partial class EventsModule
    {
        Event[] Items;
        public override async Task OnInitializing()
        {
            // var _eventsService = new EventsService();
            // var events = await _eventsService.GetEvents();
            // Items = events.ToList();
            UriBuilder builder = new UriBuilder(GlobalSettings.EventsEndpoint);
            builder.Path = "api/Events";

            string uri = builder.ToString();

            Items = await BaseApi.Get<Event[]>(uri, cacheChoice: ApiResponseCache.PreferThenUpdate, refresher: Refresh);


            await base.OnInitializing();
            await InitializeComponents();

        }

        public override Task OnPreRender()
        {
            List.Width.Set(Length.AutoStartegy.Content);
            return base.OnPreRender();
        }
        Task Refresh(Event[] items) => WhenShown(() => List.UpdateSource(Items = items));

        partial class Row
        {
            public override async Task OnInitializing()
            {
                await base.OnInitializing();
                await InitializeComponents();
                this.Width.Set(300);
                eventModule.Item = Item;
            }

            public Task RowTapped()
            {
                Nav.Go<EventSummaryPage>(new
                {
                    Id = Item.Id,
                });

                return Task.CompletedTask;
            }
        }

    }
}