using System;
using System.Threading.Tasks;
using UI;
using Zebble;


namespace Domain.Services
{
    public partial class Api : BaseApi
    {
        public static class FeedbackService
        {
            public static async Task<bool> SendIssueAsync(ReportedIssue issue)
            {
                await AddUserAndBikeIdsToIssue(issue);
                await AddLocationToIssue(issue);

                string uri = $"{GlobalSettings.IssuesEndpoint}api/Issues";
                if (await BaseApi.Post(uri, issue))
                    return true;
                return false;
            }

            private static async Task AddUserAndBikeIdsToIssue(ReportedIssue issue)
            {
                if (Settings.UserId == 0)
                {
                    throw new InvalidOperationException("UserId is not saved");
                }

                if (Settings.CurrentBookingId == 0)
                {
                    throw new InvalidOperationException("CurrentBookingId is not saved");
                }
                var currentBooking = await RidesService.GetBooking(Settings.CurrentBookingId);

                issue.BikeId = currentBooking.BikeId;
                issue.UserId = Settings.UserId;
            }

            private static async Task AddLocationToIssue(ReportedIssue issue)
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
}