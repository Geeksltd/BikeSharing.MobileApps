namespace UI.Pages
{
    using Domain;
    using System.Threading.Tasks;
    using Zebble;
    using static Domain.Services.Api;

    partial class ReportIncident
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
        }

        async Task OpenBot()
        {
            await Device.OS.OpenBrowser(GlobalSettings.SkypeBotAccount);
            return;
        }

        async Task HandlebarTapped() => SetReportType(ReportedIssueType.Handlebar);

        async Task ChainTapped() => SetReportType(ReportedIssueType.Chain);

        async Task FlatTireTapped() => SetReportType(ReportedIssueType.FlatTire);

        async Task ForkTapped() => SetReportType(ReportedIssueType.Fork);

        async Task PedalsTapped() => SetReportType(ReportedIssueType.Pedals);

        async Task LossTapped() => SetReportType(ReportedIssueType.Stolen);

        void SetReportType(ReportedIssueType type)
        {
            ChainImageView.BackgroundImagePath = "Images/Icons/report_chain.png";
            chain = false;
            Flat_tireImageView.BackgroundImagePath = "Images/Icons/report_flat_tire.png";
            flatTire = false;
            ForkImageView.BackgroundImagePath = "Images/Icons/report_fork.png";
            fork = false;
            HandlebarImageView.BackgroundImagePath = "Images/Icons/report_handlebar.png";
            handlebar = false;
            LossImageView.BackgroundImagePath = "Images/Icons/report_loss.png";
            loss = false;
            PedalsImageView.BackgroundImagePath = "Images/Icons/report_pedals.png";
            pedals = false;

            switch (type)
            {
                case ReportedIssueType.Chain:
                    ChainImageView.BackgroundImagePath = "Images/Icons/report_chain_selec.png";
                    chain = true;
                    break;
                case ReportedIssueType.FlatTire:
                    flatTire = true;
                    Flat_tireImageView.BackgroundImagePath = "Images/Icons/report_flat_tire_selec.png";
                    break;
                case ReportedIssueType.Fork:
                    ForkImageView.BackgroundImagePath = "Images/Icons/report_fork_selec.png";
                    fork = true;
                    break;
                case ReportedIssueType.Handlebar:
                    handlebar = true;
                    HandlebarImageView.BackgroundImagePath = "Images/Icons/report_handlebar_selec.png";
                    break;
                case ReportedIssueType.Stolen:
                    LossImageView.BackgroundImagePath = "Images/Icons/report_loss_selec.png";
                    loss = true;
                    break;
                case ReportedIssueType.Pedals:
                    pedals = true;
                    PedalsImageView.BackgroundImagePath = "Images/Icons/report_pedals_selec.png";
                    break;
                default: break;
            }
            reportIncidentType = type;
        }

        async Task ReportButtonTapped()
        {
            bool isValid = Validate();

            if (isValid)
            {
                var incident = new ReportedIssue
                {
                    Type = reportIncidentType,
                    Title = title,
                    Description = description
                };

                if (await FeedbackService.SendIssueAsync(incident))
                {
                    await Alert.Toast("Received");
                    ResetData();
                }
                else
                    await Alert.Toast("Error on save");
            }
            else
            {
                isValid = false;
            }
        }

        void FillData()
        {
            title = TitleInput.Text;
            description = DescriptionInput.Text;
        }

        void ResetData()
        {
            SetReportType(ReportedIssueType.Unknown);
            title = TitleInput.Text = description = DescriptionInput.Text = "";
        }
    }
}