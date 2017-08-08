using System;
using System.Linq;
using System.Threading.Tasks;
using UI;
using Zebble;

namespace Domain.Services
{
    public partial class Api : BaseApi
    {
        public static class EventsService
        {
            public static async Task<Event[]> GetEvents(Func<Event[], Task> refresher)
            {
                string uri = $"{GlobalSettings.EventsEndpoint}api/Events/";
                return await BaseApi.Get<Event[]>(uri, cacheChoice: ApiResponseCache.PreferThenUpdate, refresher: refresher);
            }
            public static async Task<Event[]> GetEvents()
            {
                string uri = $"{GlobalSettings.EventsEndpoint}api/Events/";
                return await BaseApi.Get<Event[]>(uri);
            }

            public static async Task<Event> GetEventById(int eventId)
            {
                var allEvents = await GetEvents();
                return allEvents.FirstOrDefault(e => e.Id == eventId);
            }
        }
    }
}
