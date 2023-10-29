using FlashCards.Maui.Managers.Interfaces;
using FlashCards.Maui.Pages;

namespace FlashCards.Maui
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationManager, Managers.AuthenticationManager>();

            return services;
        }

        public static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddTransient<RegisterPage>();

            return services;
        }
    }
}
