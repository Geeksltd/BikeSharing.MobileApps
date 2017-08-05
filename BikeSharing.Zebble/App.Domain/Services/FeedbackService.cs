using System;
using System.Threading.Tasks;
using UI;
using Zebble;


namespace Domain.Services
{
    public class FeedbackService : BaseApi
    {
        public async Task<bool> SendIssueAsync(ReportedIssue issue)
        {
            await AddUserAndBikeIdsToIssue(issue);
            await AddLocationToIssue(issue);

            var builder = new UriBuilder(string.Format("{0}api/Issues", GlobalSettings.IssuesEndpoint));


            string uri = builder.ToString();
            if (await BaseApi.Post(uri, issue))
                return true;
            return false;
        }

        private async Task AddUserAndBikeIdsToIssue(ReportedIssue issue)
        {
            if (Settings.UserId == 0)
            {
                throw new InvalidOperationException("UserId is not saved");
            }

            if (Settings.CurrentBookingId == 0)
            {
                throw new InvalidOperationException("CurrentBookingId is not saved");
            }
            var currentBooking = await new RidesService().GetBooking(Settings.CurrentBookingId);

            issue.BikeId = currentBooking.BikeId;
            issue.UserId = Settings.UserId;
        }

        private async Task AddLocationToIssue(ReportedIssue issue)
        {
            var location = await Device.Location.GetCurrentPosition();

            if (location != null)
            {
                issue.Latitude = location.Latitude;
                issue.Longitude = location.Longitude;
            }
        }
    }
}
