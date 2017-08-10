namespace UI.Modules
{
    using Domain;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using UI.Pages;
    using Zebble;
    using static Domain.Services.Api;

    partial class RecommendedModule
    {
        Suggestion[] Items;
        public override async Task OnInitializing()
        {
            Items = await RidesService.GetSuggestions();

            await base.OnInitializing();
            await InitializeComponents();
        }

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
                Nav.Forward<CustomRide>(new { Id = Item.Id });
                return Task.CompletedTask;
            }
        }
    }
}