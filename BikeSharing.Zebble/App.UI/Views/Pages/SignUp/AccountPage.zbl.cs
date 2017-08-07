namespace UI.Pages
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Zebble;

    partial class AccountPage
    {
        string email = string.Empty;
        string skype = string.Empty;

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            foregroundStack.Y.Set(10);


            await sunBox.Animate(new Animation
            {
                Duration = 100000.Milliseconds(),
                Change = () => sunBox.Rotation(360),
                Repeats = 60
            });
        }

        public SignUp SignupPage => FindParent<SignUp>();

        public string Email { get => email; set => email = value; }
        public string Skype { get => skype; set => skype = value; }

        async Task NextButtonTapped()
        {
            if (EmailValidation())
            {
                Email = emailInput.Text;
                Skype = skypeInput.Text;
                await SignupPage.NextPage();
            }
            else
                await Alert.Toast("Email is incorrect");
        }

        async Task TextChanged()
        {
            if (skypeInput.Text.HasValue() && EmailValidation())
                nextButton1.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            else
                nextButton1.Set(rec => rec.Enabled = false).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_disable.png");
        }


        bool EmailValidation()
        {
            if (emailInput.Text.HasValue())
            {
                string patternEmail = @"(?<email>\w+@\w+\.[a-z]{0,3})";
                var regexEmail = new Regex(patternEmail);
                if (regexEmail.IsMatch(emailInput.Text))
                    return true;
            }
            return false;
        }
    }
}