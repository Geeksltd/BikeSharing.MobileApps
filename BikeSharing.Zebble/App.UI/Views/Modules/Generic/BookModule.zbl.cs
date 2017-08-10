namespace UI.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
    using Domain;

    partial class BookModule
    {
        CustomPin from;
        CustomPin to;

        public CustomPin From { get => from; set => from = value; }
        public CustomPin To { get => to; set => to = value; }

        public override async Task OnInitializing()
        {
            From = From ?? Nav.Param<CustomPin>("from");
            To = To ?? Nav.Param<CustomPin>("to");
            await base.OnInitializing();
            await InitializeComponents();
            DateText.Text = LocalTime.Now.ToString("dddd, MMMM dd");
            FromTextView.Text = From != null ? From.Label : "";
            ToTextView.Text = To != null ? To.Label : "";
        }

        public async Task ChangeValues()
        {
            FromTextView.Text = From.Label;
            ToTextView.Text = To.Label;
        }
    }
}