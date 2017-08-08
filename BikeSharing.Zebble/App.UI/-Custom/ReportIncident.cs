using Domain;
using System;

namespace UI.Pages
{
    partial class ReportIncident
    {
        bool handlebar;
        bool fork;
        bool pedals;
        bool flatTire;
        bool chain;
        bool loss;
        ReportedIssueType reportIncidentType;
        string title;
        string description;


        public bool Validate()
        {
            bool isValidTitle = title.HasValue();
            bool isValidDescription = description.HasValue();
            bool isIncidentValid = handlebar || fork || pedals || flatTire || chain || loss;

            return isValidTitle && isValidDescription && isIncidentValid;
        }
    }
}
