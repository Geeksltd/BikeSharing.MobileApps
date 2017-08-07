namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using Zebble;

    partial class UserPage
    {
        string firstName = string.Empty;
        string lastName = string.Empty;
        DateTime birthDate = new DateTime();


        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            foregroundStack.Y.Set(10);
            birthdateInput.SelectedValue = new DateTime(1970, 01, 01);
        }
        public SignUpPage SignupPage => FindParent<SignUpPage>();

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }

        async Task NextButtonTapped()
        {
            FirstName = firstNameInput.Text;
            LastName = lastNameInput.Text;
            BirthDate = birthdateInput.SelectedValue.Value;
            await SignupPage.NextPage();
        }

        async Task TextChanged()
        {
            if (firstNameInput.Text.HasValue() && lastNameInput.Text.HasValue())
                nextButton.Set(rec => rec.Enabled = true).Background("Images/SignUp/floating_action_button_normal.png");
            else
                nextButton.Set(rec => rec.Enabled = false).Background("Images/SignUp/floating_action_button_disable.png");
        }

    }
}