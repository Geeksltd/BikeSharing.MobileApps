﻿namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;

    partial class UserPage
    {
        string _firstName = string.Empty;
        string _lastName = string.Empty;
        DateTime _birthDate = new DateTime();
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
            }
        }
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            foregroundStack.Y.Set(10);
            birthdateInput.SelectedValue = new DateTime(1970, 01, 01);
        }
        public SignUpPage SignupPage => FindParent<SignUpPage>();



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
                nextButton.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            else
                nextButton.Set(rec => rec.Enabled = false).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_disable.png");
        }

    }
}