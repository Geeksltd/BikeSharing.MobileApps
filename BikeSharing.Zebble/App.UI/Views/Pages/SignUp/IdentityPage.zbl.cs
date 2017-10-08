namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using Zebble;

    partial class IdentityPage
    {
        string userName = string.Empty;
        string password = string.Empty;



        public override async Task OnInitializing()
        {
            await base.OnInitializing();

            ForegroundStack.Y.Set(10);
            await LeftCloudBox.Animate(new Animation
            {
                Change = () => LeftCloudBox.Animate(2.Seconds(), x => x.Margin(left: 70), x => x.Visible = false),
                Repeats = 100
            });
            await RightCloudBox.Animate(new Animation
            {
                Change = () => RightCloudBox.Animate(2.Seconds(), x => x.Margin(left: 70), x => x.Visible = false),
                Repeats = 100
            });
        }
        public SignUp SignupPage => FindParent<SignUp>();

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }

        async Task NextButtonTapped()
        {
            if (UsernameInput.Text.HasValue() && PasswordInput.Text.HasValue() && RepeatPasswordInput.Text.HasValue() && PasswordInput.Text == RepeatPasswordInput.Text)
            {
                UserName = UsernameInput.Text;
                Password = PasswordInput.Text;
                await SignupPage.NextPage();
            }
        }

        async Task TextChanged()
        {
            if (UsernameInput.Text.HasValue() && PasswordInput.Text.HasValue() && RepeatPasswordInput.Text.HasValue() && RepeatPasswordInput.Text == PasswordInput.Text)
                NextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/Icons/floating_action_button_normal.png");
            else
                NextButton.Set(rec => rec.Enabled = false).Set(rec => rec.BackgroundImagePath = "Images/Icons/floating_action_button_disable.png");
        }

        async Task RightCloudBoxVisibilityChanged()
        {
            if (RightCloudBox.Visible == false)
                if (RightCloudBox.Margin.Left.CurrentValue == 70)
                {
                    RightCloudBox.Visible = true;
                    await Task.Delay(1000);


                    await RightCloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => RightCloudBox.Animate(10.Seconds(), x => x.Margin(left: 1))
                    });
                    RightCloudBox.Visible = false;
                }
                else if (RightCloudBox.Margin.Left.CurrentValue == 1)
                {
                    RightCloudBox.Visible = true;
                    await Task.Delay(1000);
                    await RightCloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => RightCloudBox.Animate(10.Seconds(), x => x.Margin(left: 70))
                    });
                    RightCloudBox.Visible = false;
                }
        }

        async Task LeftCloudBoxVisibilityChanged()
        {
            if (LeftCloudBox.Visible == false)
                if (LeftCloudBox.Margin.Left.CurrentValue == 70)
                {
                    LeftCloudBox.Visible = true;
                    await Task.Delay(1000);


                    await LeftCloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => LeftCloudBox.Animate(10.Seconds(), x => x.Margin(left: 1))
                    });
                    LeftCloudBox.Visible = false;
                }
                else if (LeftCloudBox.Margin.Left.CurrentValue == 1)
                {
                    LeftCloudBox.Visible = true;
                    await Task.Delay(1000);
                    await LeftCloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => LeftCloudBox.Animate(10.Seconds(), x => x.Margin(left: 70))
                    });
                    LeftCloudBox.Visible = false;
                }
        }
    }
}