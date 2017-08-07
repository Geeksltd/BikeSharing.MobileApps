namespace UI.Pages
{
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Services;
    using Zebble;

    partial class SignUpPage
    {
        IdentityPage identityPage;
        AccountPage accountPage;
        GenderPage genderPage;
        UserPage userPage;
        SubscriptionPage subscriptionPage;

        public SubscriptionPage SubscriptionPage { get => subscriptionPage; set => subscriptionPage = value; }
        public UserPage UserPage { get => userPage; set => userPage = value; }
        public GenderPage GenderPage { get => genderPage; set => genderPage = value; }
        public AccountPage AccountPage { get => accountPage; set => accountPage = value; }
        public IdentityPage IdentityPage { get => identityPage; set => identityPage = value; }

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            var user = new UserAndProfileModel();
            IdentityPage = new IdentityPage();

            AccountPage = new AccountPage();
            GenderPage = new GenderPage();
            UserPage = new UserPage();
            SubscriptionPage = new SubscriptionPage();
            await MyCarousel.Slides.Add(IdentityPage);
            await MyCarousel.Slides.Add(AccountPage);
            await MyCarousel.Slides.Add(GenderPage);
            await MyCarousel.Slides.Add(UserPage);
            await MyCarousel.Slides.Add(SubscriptionPage);
        }

        public async Task NextPage() => await MyCarousel.Next(animate: true);

        public async Task SaveUserData()
        {
            //   try
            //  {
            //var CreditCard = "01234567890";
            //var CreditCardType = 0;
            //var ExpirationDate = DateTime.Now.AddYears(1);

            var userAndProfile = new UserAndProfileModel
            {
                UserName = IdentityPage.UserName,
                Password = IdentityPage.Password,
                Gender = GenderPage.Gender == false ? "Male" : "Female",
                BirthDate = UserPage.BirthDate,
                FirstName = UserPage.FirstName,
                LastName = UserPage.LastName,
                Email = AccountPage.Email,
                Skype = AccountPage.Skype,
                TenantId = GlobalSettings.TenantId
            };



            var result = await new ProfileService().SignUp(userAndProfile);

            if (result != null)
            {
                bool isAuthenticated =
                    await new AuthenticationService().LoginAsync(userAndProfile.UserName, userAndProfile.Password);


                if (isAuthenticated)
                {
                    await Nav.Go<HomePage>();
                }
                else
                {
                    await Alert.Show("Invalid credentials", "Login failure");
                }
            }
            else
            {
                await Alert.Show("Invalid data", "Sign Up failure");
            }
            // }
            // catch (Exception ex)
            // {
            //    Console.WriteLine($"Error reporting incident in: {ex}");
            //    await Alert.Show("Invalid data", "Sign Up failure");
            // }
        }

    }
}