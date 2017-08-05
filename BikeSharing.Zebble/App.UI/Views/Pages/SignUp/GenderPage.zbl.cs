namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using Zebble;

    partial class GenderPage
    {
        bool _gender = false;
        public bool Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

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

        public SignUpPage SignupPage => FindParent<SignUpPage>();

        async Task ImageViewTapped()
        {
            nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            if (Gender)
            {
                Gender = false;
                womanImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_woman_select.png");
                manImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_man.png");
            }
            else
            {
                Gender = true;
                womanImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_woman.png");
                manImageView.Set(rec => rec.BackgroundImagePath = "Images/SignUp/signup_man_select.png");
            }
        }

        async Task NextButtonTapped() => await SignupPage.NextPage();
    }
}