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