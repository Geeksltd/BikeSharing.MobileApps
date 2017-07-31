namespace UI.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
     
    using Domain;
    using Domain.Services;
    using Domain.Entities;
    using System.Collections.ObjectModel;
    using UI.Pages;

    partial class RecommendedModule
    {
        Suggestion[] Items;
        public override async Task OnInitializing()
        {
             var _ridesService = new RidesService();
             var rides = await _ridesService.GetSuggestions();
             Items = rides.ToArray();

      
            await base.OnInitializing();
            await InitializeComponents();

        
        }

        //  Task Refresh(Suggestion[] items) => WhenShown(() => List.UpdateSource(Items = items));
        public override Task OnPreRender()
        {
            List.Width.Set(Length.AutoStartegy.Content);
            return base.OnPreRender();
        }
        partial class Row
        {
            public override async Task OnInitializing()
            {
                await base.OnInitializing();
                await InitializeComponents();
                this.Width.Set(400);
            }

            public Task RowTapped()
            {
                Nav.Forward<CustomRidePage>(new
                {

                });

                return Task.CompletedTask;
            }
        }

    }
}