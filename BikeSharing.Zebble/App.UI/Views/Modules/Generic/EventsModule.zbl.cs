﻿namespace UI.Modules
{
    using Domain;
    using System;
    using System.Threading.Tasks;
    using UI.Pages;
    using Zebble;
    using static Domain.Services.Api;

    partial class EventsModule
    {
        Event[] Items;
        public override async Task OnInitializing()
        {
            Items = await EventsService.GetEvents(refresher: Refresh); // BaseApi.Get<Event[]>(uri, cacheChoice: ApiResponseCache.PreferThenUpdate, refresher: Refresh);

            await base.OnInitializing();
         
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
             
                this.Width.Set(300);
                EventModule.Item = Item;
            }

            public Task RowTapped()
            {
                Nav.Go<EventSummary>(new { Id = Item.Id });

                return Task.CompletedTask;
            }
        }
    }
}