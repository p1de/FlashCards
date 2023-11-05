using FlashCards.Core.Application.Common.Interfaces.Authentication;
using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Core.Application.Common.Interfaces.Services;
using FlashCards.Infrastructure.Authentication;
using FlashCards.Infrastructure.Common.Persistance;
using FlashCards.Infrastructure.Common.Persistance.Interfaces;
using FlashCards.Infrastructure.Common.Persistance.MongoDb;
using FlashCards.Infrastructure.Common.Persistance.SQLiteDb;
using FlashCards.Infrastructure.Services;

namespace FlashCards.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IOnlineDbContext, MongoDbContext>();
            services.AddSingleton<IOfflineDbContext, SQLiteDbContext>();
            services.AddScoped(typeof(IOnlineGenericRepository<>), typeof(OnlineGenericRepository<>));
            services.AddScoped(typeof(IOfflineGenericRepository<>), typeof(OfflineGenericRepository<>));

            return services;
        }
    }
}