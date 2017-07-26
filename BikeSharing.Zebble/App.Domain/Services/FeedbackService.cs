using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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

            UriBuilder builder = new UriBuilder(GlobalSettings.IssuesEndpoint);
            builder.Path = "api/Issues";

            string uri = builder.ToString();
            if (await Api.Post(uri, issue))
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
            var _ridesService = new RidesService();
            Booking currentBooking = await _ridesService.GetBooking(Settings.CurrentBookingId);

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
