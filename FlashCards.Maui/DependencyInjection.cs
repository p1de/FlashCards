using FlashCards.Maui.Managers.Interfaces;
using FlashCards.Maui.Pages.Dashboard;
using FlashCards.Maui.Pages.Startup;
using FlashCards.Maui.ViewModels;
using FlashCards.Maui.ViewModels.Dashboard;
using FlashCards.Maui.ViewModels.Startup;

namespace FlashCards.Maui
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationManager, Managers.AuthenticationManager>();

            return services;
        }

        public static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddTransient<LoginPage>();
            services.AddTransient<RegisterPage>();
            services.AddTransient<DashboardPage>();

            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<RegisterPageViewModel>();
            services.AddTransient<DashboardPageViewModel>();

            return services;
        }
    }
}
