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
            foregroundStack.Y.Set(10);

            await cloudBox.Animate(new Animation
            {
                Change = () => cloudBox.Animate(2.Seconds(), x => x.Margin(left: 70), x => x.Visible = false),
                Repeats = 100
            });
        }

        async Task CloudBoxVisibilityChanged()
        {
            if (cloudBox.Visible == false)
                if (cloudBox.Margin.Left.CurrentValue == 70)
                {
                    cloudBox.Visible = true;
                    await Task.Delay(1000);


                    await cloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => cloudBox.Animate(10.Seconds(), x => x.Margin(left: 1))
                    });
                    cloudBox.Visible = false;
                }
                else if (cloudBox.Margin.Left.CurrentValue == 1)
                {
                    cloudBox.Visible = true;
                    await Task.Delay(1000);
                    await cloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => cloudBox.Animate(10.Seconds(), x => x.Margin(left: 70))
                    });
                    cloudBox.Visible = false;
                }
        }

        public SignUp SignupPage => FindParent<SignUp>();

        public bool Gender { get => gender; set => gender = value; }

        async Task ImageViewTapped()
        {
            nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            if (Gender)
            {
                Gender = false;
                womanImageView.Path("Images/SignUp/signup_woman_select.png");
                manImageView.Path("Images/SignUp/signup_man.png");
            }
            else
            {
                Gender = true;
                womanImageView.Path("Images/SignUp/signup_woman.png");
                manImageView.Path("Images/SignUp/signup_man_select.png");
            }
        }

        async Task NextButtonTapped() => await SignupPage.NextPage();
    }
}