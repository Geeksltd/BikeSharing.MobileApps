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
    using Domain.Services;

    partial class SubscriptionPage
    {
      

        public enum Subscription
        {
            Monthly,
            ThreeMonthly,
            Annual
        };
        Subscription _subscription;


       

        public SignUpPage signupPage => FindParent<SignUpPage>();


        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

        }


        async Task NextButtonTapped()
        {
            signupPage.SaveUserData();
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
                _subscription = Subscription.Monthly;           
                monthlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_monthly_active.png");
                quarterlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_quarterly_normal.png");
                annualImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_annual_normal.png");
            }
            else if (item == Subscription.ThreeMonthly)
            {
                _subscription = Subscription.ThreeMonthly;
                 monthlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_monthly_normal.png");
                quarterlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_quarterly_active.png");
                annualImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_annual_normal.png");

            }
            else if (item == Subscription.Annual)
            {
                _subscription = Subscription.Annual;
                monthlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_monthly_normal.png");
                quarterlyImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_quarterly_normal.png");
                annualImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_annual_active.png");
            }            
        }

    }
}