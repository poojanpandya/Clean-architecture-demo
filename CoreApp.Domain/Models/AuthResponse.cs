using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Domain.Models
{
    public class AuthResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime expiration { get; set; }
        public string UserId { get; set; }
        public List<string> Errors { get; set; }
    }
}
