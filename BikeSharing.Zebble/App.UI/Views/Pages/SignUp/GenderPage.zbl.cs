namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using Zebble;

    partial class GenderPage
    {
        bool gender = false;

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            ForegroundStack.Y.Set(10);

            await CloudBox.Animate(new Animation
            {
                Change = () => CloudBox.Animate(2.Seconds(), x => x.Margin(left: 70), x => x.Visible = false),
                Repeats = 100
            });
        }

        async Task CloudBoxVisibilityChanged()
        {
            if (CloudBox.Visible == false)
                if (CloudBox.Margin.Left.CurrentValue == 70)
                {
                    CloudBox.Visible = true;
                    await Task.Delay(1000);


                    await CloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => CloudBox.Animate(10.Seconds(), x => x.Margin(left: 1))
                    });
                    CloudBox.Visible = false;
                }
                else if (CloudBox.Margin.Left.CurrentValue == 1)
                {
                    CloudBox.Visible = true;
                    await Task.Delay(1000);
                    await CloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => CloudBox.Animate(10.Seconds(), x => x.Margin(left: 70))
                    });
                    CloudBox.Visible = false;
                }
        }

        public SignUp SignupPage => FindParent<SignUp>();

        public bool Gender { get => gender; set => gender = value; }

        async Task ImageViewTapped()
        {
            NextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            if (Gender)
            {
                Gender = false;
                WomanImageView.Path("Images/SignUp/signup_woman_select.png");
                ManImageView.Path("Images/SignUp/signup_man.png");
            }
            else
            {
                Gender = true;
                WomanImageView.Path("Images/SignUp/signup_woman.png");
                ManImageView.Path("Images/SignUp/signup_man_select.png");
            }
        }

        async Task NextButtonTapped() => await SignupPage.NextPage();
    }
}