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
            await InitializeComponents();
            foregroundStack.Y.Set(10);
            await leftCloudBox.Animate(new Animation
            {
                Change = () => leftCloudBox.Animate(2.Seconds(), x => x.Margin(left: 70), x => x.Visible = false),
                Repeats = 100
            });
            await RightCloudBox.Animate(new Animation
            {
                Change = () => RightCloudBox.Animate(2.Seconds(), x => x.Margin(left: 70), x => x.Visible = false),
                Repeats = 100
            });
        }
        public SignUpPage SignupPage => FindParent<SignUpPage>();

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }

        async Task NextButtonTapped()
        {
            if (usernameInput.Text.HasValue() && passwordInput.Text.HasValue() && repeatPasswordInput.Text.HasValue() && passwordInput.Text == repeatPasswordInput.Text)
            {
                UserName = usernameInput.Text;
                Password = passwordInput.Text;
                await SignupPage.NextPage();
            }
        }

        async Task TextChanged()
        {
            if (usernameInput.Text.HasValue() && passwordInput.Text.HasValue() && repeatPasswordInput.Text.HasValue() && repeatPasswordInput.Text == passwordInput.Text)
                nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            else
                nextButton.Set(rec => rec.Enabled = false).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_disable.png");
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
            if (leftCloudBox.Visible == false)
                if (leftCloudBox.Margin.Left.CurrentValue == 70)
                {
                    leftCloudBox.Visible = true;
                    await Task.Delay(1000);


                    await leftCloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => leftCloudBox.Animate(10.Seconds(), x => x.Margin(left: 1))
                    });
                    leftCloudBox.Visible = false;
                }
                else if (leftCloudBox.Margin.Left.CurrentValue == 1)
                {
                    leftCloudBox.Visible = true;
                    await Task.Delay(1000);
                    await leftCloudBox.Animate(new Animation
                    {
                        Duration = 1.Seconds(),
                        Change = () => leftCloudBox.Animate(10.Seconds(), x => x.Margin(left: 70))
                    });
                    leftCloudBox.Visible = false;
                }
        }

    }
}