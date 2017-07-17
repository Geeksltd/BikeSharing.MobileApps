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
    using System.Collections.ObjectModel;
    using UI.Pages;

    partial class RecommendedModule
    {
        public List<Suggestion> Items;
        public override async Task OnInitializing()
        {
            var _ridesService = new RidesService();
            var rides = await _ridesService.GetSuggestions();
            Items = rides.ToList();
            await base.OnInitializing();
            await InitializeComponents();
        }



        partial class Row
        {
            public override async Task OnInitializing()
            {
                await base.OnInitializing();
                await InitializeComponents();
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