using System;
using Domain;

namespace UI.Pages
{
    partial class ReportIncidentPage
    {

        bool _handlebar;
        bool _fork;
        bool _pedals;
        bool _flatTire;
        bool _chain;
        bool _loss;
        ReportedIssueType _reportIncidentType;
        string _title;
        string _description;
        bool _isValid;

        public bool Handlebar
        {
            get
            {
                return _handlebar;
            }
            set
            {
                _handlebar = value;
            }
        }

        public bool Fork
        {
            get
            {
                return _fork;
            }
            set
            {
                _fork = value;
            }
        }

        public bool Pedals
        {
            get
            {
                return _pedals;
            }
            set
            {
                _pedals = value;
            }
        }

        public bool FlatTire
        {
            get
            {
                return _flatTire;
            }
            set
            {
                _flatTire = value;
            }
        }

        public bool Chain
        {
            get
            {
                return _chain;
            }
            set
            {
                _chain = value;
            }
        }

        public bool Loss
        {
            get
            {
                return _loss;
            }
            set
            {
                _loss = value;
            }
        }

        public string ReportTitle
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
            }
        }


        public bool Validate()
        {
            bool isValidTitle = _title.HasValue();
            bool isValidDescription = _description.HasValue();
            bool isIncidentValid = _handlebar || _fork || _pedals || _flatTire || _chain || _loss;

            return isValidTitle && isValidDescription && isIncidentValid;
        }

    }
}
