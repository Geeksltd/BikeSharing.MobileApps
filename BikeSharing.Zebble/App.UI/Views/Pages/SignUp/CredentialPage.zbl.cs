namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
     
    using Domain;

    partial class CredentialPage
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
        }

        async Task NextButtonTapped()
        {
        }
    }
}