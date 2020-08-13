using CoreApp.Application.ViewModels;
using CoreApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.Interfaces
{
    public interface IAuthService
    {
       Task<AuthResponse> RegisterAsync(RegisterModel Model);
    }
}
