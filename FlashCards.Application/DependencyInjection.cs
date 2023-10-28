using FlashCards.Core.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}