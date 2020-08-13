using CoreApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.ViewModels
{
    public class AuthViewModel
    {
        public bool Status { get; set; }

        public string Message { get; set; }
        
        public AuthResponse authResponse { get; set; }
  
    }
}
