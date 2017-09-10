namespace UI.Pages
{
    using Domain.Entities;
    using System.Threading.Tasks;
    using Zebble;
    using static Domain.Services.Api;

    partial class SignUp
    {
        IdentityPage IdentityPage;
        AccountPage AccountPage;
        GenderPage GenderPage;
        UserPage UserPage;
        SubscriptionPage SubscriptionPage;

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
           
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

            var result = await ProfileService.SignUpAsync(userAndProfile);

            if (result != null)
            {
                bool isAuthenticated =
                 await AuthenticationService.LoginAsync(userAndProfile.UserName, userAndProfile.Password);


                if (isAuthenticated)
                {
                    await Nav.Go<Home>();
                }
                else
                {
                    await Alert.Show("Invalid credentials", "Login failure");
                }
            }
        }
    }
}