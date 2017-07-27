namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
     
    using Domain;
    using Domain.Entities;

    partial class GenderPage
    {
        UserAndProfileModel user;
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

             user = Nav.Param<UserAndProfileModel>("UserAndProfileModel");

        }


        async Task NextButtonTapped()
        {
         
            user.Gender = "0";
           
            Nav.Forward<UserPage>(new { UserAndProfileModel = user });
        }

    }
}