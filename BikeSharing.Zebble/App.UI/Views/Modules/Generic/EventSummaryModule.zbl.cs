namespace UI.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
    using Zebble.Framework;
    using Domain;
    using Domain.Services;
    using Domain.Entities;
    using UI.Pages;
    using UI._Custom;

    partial class EventSummaryModule
    {

        List< Event> Items;
        public override async Task OnInitializing()
        {
            try
            {
                // var _eventsService = new EventsService();
                // var events = await _eventsService.GetEvents();
                // Items = events.ToList();
                UriBuilder builder = new UriBuilder(GlobalSettings.EventsEndpoint);
                builder.Path = "api/Events";

                string uri = builder.ToString();

                Items = await Api.Get <List<Event>>(uri , cacheChoice:ApiResponseCache.PreferThenUpdate, refresher : Refresh);

            }catch(Exception ex)
            {


            }
            await base.OnInitializing();
            await InitializeComponents();

         
        }

        Task Refresh(List<Event> items) => WhenShown(() => List.UpdateSource(Items = items));

        partial class Row
        {
            public override async Task OnInitializing()
            {
                await base.OnInitializing();
                await InitializeComponents();               
                this.Width.Set(300);
            }

            public Task RowTapped()
            {
                Nav.Forward<EventSummaryPage>(new
                {
                   
                });

                return Task.CompletedTask;
            }
        }

    }
}