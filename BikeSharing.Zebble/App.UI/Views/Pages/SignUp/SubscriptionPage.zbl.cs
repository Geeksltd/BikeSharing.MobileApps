namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using Zebble;

    partial class SubscriptionPage
    {
        public enum Subscription
        {
            Monthly,
            ThreeMonthly,
            Annual
        };
        Subscription _subscription;
        public SignUp SignupPage => FindParent<SignUp>();

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            foregroundStack.Y.Set(10);

            await sunBox.Animate(new Animation
            {
                Duration = 100000.Milliseconds(),
                Change = () => sunBox.Rotation(360),
                Repeats = 600
            });

            await cloudBox.Animate(new Animation
            {
                Change = () => cloudBox.Animate(2.Seconds(), x => x.Margin(left: 150), x => x.Visible = false),
                Repeats = 100
            });
        }

        async Task CloudBoxVisibilityChanged()
        {
            if (cloudBox.Visible == false)
                if (cloudBox.Margin.Left.CurrentValue == 150)
                {
                    cloudBox.Visible = true;
                    await Task.Delay(1000);


                    await cloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => cloudBox.Animate(10.Seconds(), x => x.Margin(left: 50))
                    });
                    cloudBox.Visible = false;
                }
                else if (cloudBox.Margin.Left.CurrentValue == 50)
                {
                    cloudBox.Visible = true;
                    await Task.Delay(1000);
                    await cloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => cloudBox.Animate(10.Seconds(), x => x.Margin(left: 150))
                    });
                    cloudBox.Visible = false;
                }
        }

        async Task NextButtonTapped() => await SignupPage.SaveUserData();

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
            switch (item)
            {
                case Subscription.Monthly:
                    _subscription = Subscription.Monthly;
                    monthlyImageView.Path("Images/SignUp/signup_monthly_active.png");
                    quarterlyImageView.Path("Images/SignUp/signup_quarterly_normal.png");
                    annualImageView.Path("Images/SignUp/signup_annual_normal.png");
                    break;
                case Subscription.ThreeMonthly:
                    _subscription = Subscription.ThreeMonthly;
                    monthlyImageView.Path("Images/SignUp/signup_monthly_normal.png");
                    quarterlyImageView.Path("Images/SignUp/signup_quarterly_active.png");
                    annualImageView.Path("Images/SignUp/signup_annual_normal.png");
                    break;
                case Subscription.Annual:
                    _subscription = Subscription.Annual;
                    monthlyImageView.Path("Images/SignUp/signup_monthly_normal.png");
                    quarterlyImageView.Path("Images/SignUp/signup_quarterly_normal.png");
                    annualImageView.Path("Images/SignUp/signup_annual_active.png");
                    break;
                default:
                    break;
            }
        }
    }
}