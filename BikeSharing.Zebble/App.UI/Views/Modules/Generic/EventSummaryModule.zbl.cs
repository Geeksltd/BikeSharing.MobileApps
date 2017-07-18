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

    partial class EventSummaryModule
    {

        public List<Event> Items;
        public override async Task OnInitializing()
        {
            var _eventsService = new EventsService();
            var events = await _eventsService.GetEvents();
            Items = events.ToList();
          
            await base.OnInitializing();
            await InitializeComponents();

         
        }

        

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