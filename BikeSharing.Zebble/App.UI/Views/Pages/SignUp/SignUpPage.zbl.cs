namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;

    using Domain;
    using Domain.Entities;
    using Domain.Services;

    partial class SignUpPage
    {

        UsenamePage usenamePage;
        AccountPage accountPage;
        GenderPage genderPage;
        UserPage userPage;
        SubscriptionPage subscriptionPage;
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            var user = new UserAndProfileModel();
            usenamePage = new UsenamePage();

            accountPage = new AccountPage();
            genderPage = new GenderPage();
            userPage = new UserPage();
            subscriptionPage = new SubscriptionPage();
            await MyCarousel.Slides.Add(usenamePage);
            await MyCarousel.Slides.Add(accountPage);
            await MyCarousel.Slides.Add(genderPage);
            await MyCarousel.Slides.Add(userPage);
            await MyCarousel.Slides.Add(subscriptionPage);            
        }

        public async Task NextPage()
        {
            await MyCarousel.Next(true);
        }

        public async Task SaveUserData()
        {
            try
            {
                //var CreditCard = "01234567890";
                //var CreditCardType = 0;
                //var ExpirationDate = DateTime.Now.AddYears(1);

                var userAndProfile = new UserAndProfileModel
                {
                    UserName = usenamePage.UserName,
                    Password = usenamePage.Password,
                    Gender = genderPage.Gender == false ?  "Male": "Female",
                    BirthDate = userPage.BirthDate,
                    FirstName = userPage.FirstName,
                    LastName = userPage.LastName,
                    Email = accountPage.Email,
                    Skype = accountPage.Skype,
                    TenantId = GlobalSettings.TenantId
                };
               

                var _profileService = new ProfileService();

                UserAndProfileModel result = await _profileService.SignUp(userAndProfile);

                if (result != null)
                {
                    var _authenticationService = new AuthenticationService();
                    bool isAuthenticated =
                        await _authenticationService.LoginAsync(userAndProfile.UserName, userAndProfile.Password);

                    if (isAuthenticated)
                    {
                        await Nav.Go<HomePage>();
                    }
                    else
                    {
                        Alert.Show("Invalid credentials", "Login failure");
                    }
                }
                else
                {
                    Alert.Show("Invalid data", "Sign Up failure");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reporting incident in: {ex}");

                Alert.Show("Invalid data", "Sign Up failure");
            }
        }

    }
}