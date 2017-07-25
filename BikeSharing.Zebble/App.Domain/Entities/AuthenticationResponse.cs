using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AuthenticationResponse
    {
        public int UserId { get; set; }

        public int ProfileId { get; set; }

        public string AccessToken { get; set; }
    }
}
