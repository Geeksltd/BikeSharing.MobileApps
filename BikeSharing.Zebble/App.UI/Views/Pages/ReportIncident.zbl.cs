namespace UI.Pages
{
    using System.Threading.Tasks;
    using Domain;
    using Domain.Services;
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
            // try
            //  {
            await Device.OS.OpenBrowser(GlobalSettings.SkypeBotAccount);
            //  }
            //   catch (Exception ex)
            //   {
            //       Console.Write(ex.Message);
            //       await Alert.Show("Error", "Unable to launch Skype.", KeyValuePair.Of("Ok", true));
            //   }
        }

        async Task HandlebarTapped()
        {
            SetReportType(ReportedIssueType.Handlebar);
            //   handlebarImageView.Set(x => x.Style.BackgroundColor = "Red"); 
        }
        async Task ChainTapped() => SetReportType(ReportedIssueType.Chain);

        async Task FlatTireTapped() => SetReportType(ReportedIssueType.FlatTire);

        async Task ForkTapped() => SetReportType(ReportedIssueType.Fork);

        async Task PedalsTapped() => SetReportType(ReportedIssueType.Pedals);

        async Task LossTapped() => SetReportType(ReportedIssueType.Stolen);

        void SetReportType(ReportedIssueType type)
        {
            chainImageView.BackgroundImagePath = "Images/ic_report_chain.png";
            Chain = false;
            flat_tireImageView.BackgroundImagePath = "Images/ic_report_flat_tire.png";
            FlatTire = false;
            forkImageView.BackgroundImagePath = "Images/ic_report_fork.png";
            Fork = false;
            handlebarImageView.BackgroundImagePath = "Images/ic_report_handlebar.png";
            Handlebar = false;
            lossImageView.BackgroundImagePath = "Images/ic_report_loss.png";
            Loss = false;
            pedalsImageView.BackgroundImagePath = "Images/ic_report_pedals.png";
            Pedals = false;

            switch (type)
            {
                case ReportedIssueType.Chain:
                    chainImageView.BackgroundImagePath = "Images/ic_report_chain_selec.png";
                    Chain = true;
                    break;
                case ReportedIssueType.FlatTire:
                    FlatTire = true;
                    flat_tireImageView.BackgroundImagePath = "Images/ic_report_flat_tire_selec.png";
                    break;
                case ReportedIssueType.Fork:
                    forkImageView.BackgroundImagePath = "Images/ic_report_fork_selec.png";
                    Fork = true;
                    break;
                case ReportedIssueType.Handlebar:
                    Handlebar = true;
                    handlebarImageView.BackgroundImagePath = "Images/ic_report_handlebar_selec.png";
                    break;
                case ReportedIssueType.Stolen:
                    lossImageView.BackgroundImagePath = "Images/ic_report_loss_selec.png";
                    Loss = true;
                    break;
                case ReportedIssueType.Pedals:
                    Pedals = true;
                    pedalsImageView.BackgroundImagePath = "Images/ic_report_pedals_selec.png";
                    break;
                default:break;
            }

            _reportIncidentType = type;
        }

        async Task ReportButtonTapped()
        {
            // IsBusy = true;
            FillData();
            //try
            //{
            IsValid = true;

            bool isValid = Validate();

            if (isValid)
            {
                var incident = new ReportedIssue
                {
                    Type = _reportIncidentType,
                    Title = Title,
                    Description = Description
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
                IsValid = false;
            }
            //}
            //catch (Exception ex) when (ex is WebException)
            //{
            //    await Alert.Show("Error", "Communication error");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error reporting incident in: {ex}");
            //}
            // IsBusy = false;
        }

        void FillData()
        {
            Title = titleInput.Text;
            Description = descriptionInput.Text;
        }

        void ResetData()
        {
            SetReportType(ReportedIssueType.Unknown);
            Title = titleInput.Text = Description = descriptionInput.Text = "";
        }

    }
}