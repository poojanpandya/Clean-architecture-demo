using CoreApp.Application.Interfaces;
using CoreApp.Application.Services;
using CoreApp.Infra.Data.Interfaces;
using CoreApp.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApp.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //CleanArchitecture.Application
            services.AddScoped<IBookService, BookService>();

            //CleanArchitecture.Domain.Interfaces | CleanArchitecture.Infra.Data.Repositories
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}
