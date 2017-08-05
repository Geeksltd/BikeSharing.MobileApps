using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI;
using Zebble;

namespace Domain.Services
{
    public class EventsService : BaseApi
    {
        public async Task<Event[]> GetEvents()
        {
            var builder = new UriBuilder(string.Format("{0}api/Events/", GlobalSettings.EventsEndpoint));
            string uri = builder.ToString();
            return await BaseApi.Get<Event[]>(uri);
        }

        public async Task<Event> GetEventById(int eventId)
        {
            var allEvents = await GetEvents();
            return allEvents.FirstOrDefault(e => e.Id == eventId);
        }
    }
}
