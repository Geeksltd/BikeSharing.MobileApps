using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    public class StaticData
    {
        public static List<Suggestion> GetSuggestions()
        {
            var suggestions = new List<Suggestion>()
            {
                new Suggestion
                {
                    Name = "Beacon Hill",
                    Distance = 1900,
                    ImagePath = "Images/suggestion_beacon_hill.png",
                    Latitude = 47.608013f,
                    Longitude = -122.9675438f
                },
                new Suggestion
                {
                    Name = "Golden Gardens",
                    Distance = 2200,
                    ImagePath = "Images/suggestion_golden_gardens.png",
                    Latitude = 47.7397176f,
                    Longitude = -122.8429737f
                },
                new Suggestion
                {
                    Name = "Lake Union Loop",
                    Distance = 3500,
                    ImagePath = "Images/suggestion_lake_union_loop.png",
                    Latitude = 47.703336f,
                    Longitude = -122.8429737f
                }
            };
         
            return suggestions;
        }
    }
}
