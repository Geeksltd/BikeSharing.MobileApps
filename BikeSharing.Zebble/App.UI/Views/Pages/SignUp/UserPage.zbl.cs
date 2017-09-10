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
         
            ForegroundStack.Y.Set(10);
            BirthdateInput.SelectedValue = new DateTime(1970, 01, 01);
        }
        public SignUp SignupPage => FindParent<SignUp>();

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }

        async Task NextButtonTapped()
        {
            FirstName = FirstNameInput.Text;
            LastName = LastNameInput.Text;
            BirthDate = BirthdateInput.SelectedValue.Value;
            await SignupPage.NextPage();
        }

        async Task TextChanged()
        {
            if (FirstNameInput.Text.HasValue() && LastNameInput.Text.HasValue())
                NextButton.Set(rec => rec.Enabled = true).Background("Images/Icons/floating_action_button_normal.png");
            else
                NextButton.Set(rec => rec.Enabled = false).Background("Images/Icons/floating_action_button_disable.png");
        }
    }
}