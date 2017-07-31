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

    partial class SubscriptionPage
    {
        UserAndProfileModel user;


        public enum Subscription
        {
            Monthly,
            ThreeMonthly,
            Annual
        };
        Subscription subscription;


        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

           // user = Nav.Param<UserAndProfileModel>("UserAndProfileModel");
            subscription = new Subscription();

        }


        async Task NextButtonTapped()
        {

            try {

                //var CreditCard = "01234567890";
                // var   CreditCardType = 0,
                // var   ExpirationDate = DateTime.Now.AddYears(1)
               

                //var userAndProfile = new UserAndProfileModel
                //{
                //    UserName = Profile.User.UserName,
                //    Password = Profile.User.Password,
                //    Gender = gender,
                //    BirthDate = Profile.BirthDate,
                //    FirstName = Profile.FirstName,
                //    LastName = Profile.LastName,
                //    Email = Profile.Email,
                //    Skype = Profile.Skype,
                //    TenantId = GlobalSettings.TenantId
                //};

                //IsBusy = true;

                //UserAndProfileModel result = await _profileService.SignUp(userAndProfile);

                //if (result != null)
                //{
                //    bool isAuthenticated =
                //        await _authenticationService.LoginAsync(userAndProfile.UserName, userAndProfile.Password);

                //    if (isAuthenticated)
                //    {
                //        await NavigationService.NavigateToAsync<MainViewModel>();
                //    }
                //    else
                //    {
                //        await DialogService.ShowAlertAsync("Invalid credentials", "Login failure", "Try again");
                //    }
                //}
                //else
                //{
                //    await DialogService.ShowAlertAsync("Invalid data", "Sign Up failure", "Try again");
                //}
            }
            catch (Exception ex)
            {
              //  Debug.WriteLine($"Exception in sign up {ex}");
                //await DialogService.ShowAlertAsync("Invalid data", "Sign Up failure", "Try again");
            }


        }

        async Task AnnualImageViewTapped()
        {
            nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            SetSubscription(Subscription.Annual);
        }
        async Task MonthlyImageViewTapped()
        {
            nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            SetSubscription(Subscription.Monthly);
        }
        async Task ThreeMonthlyImageViewTapped()
        {
            nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            SetSubscription(Subscription.ThreeMonthly);
        }

        void SetSubscription(Subscription item)
        {
          if (item == Subscription.Monthly)
            {
                subscription = Subscription.Monthly;           
                monthlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_monthly_active.png");
                quarterlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_quarterly_normal.png");
                annualImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_annual_normal.png");
            }
            else if (item == Subscription.ThreeMonthly)
            {
                subscription = Subscription.ThreeMonthly;
                 monthlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_monthly_normal.png");
                quarterlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_quarterly_active.png");
                annualImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_annual_normal.png");

            }
            else if (item == Subscription.Annual)
            {
                subscription = Subscription.Annual;
                monthlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_monthly_normal.png");
                quarterlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_quarterly_normal.png");
                annualImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_annual_active.png");
            }            
        }

    }
}