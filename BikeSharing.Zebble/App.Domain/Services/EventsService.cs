using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;

namespace Domain.Services
{
    public class EventsService : BaseApi
    {
        public async Task<IEnumerable<Event>> GetEvents()
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.EventsEndpoint);
            builder.Path = "api/Events";

            string uri = builder.ToString();

            IEnumerable<Event> events = await Api.Get<IEnumerable<Event>>(uri);

            return events;
        }

        public async Task<Event> GetEventById(int eventId)
        {
            var allEvents = await GetEvents();

            return allEvents.FirstOrDefault(e => e.Id == eventId);
        }
    }
}
