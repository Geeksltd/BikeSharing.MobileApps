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
        public SignUpPage signupPage => FindParent<SignUpPage>();

        async Task NextButtonTapped()
        {
            await signupPage.NextPage();
        }
    }
}