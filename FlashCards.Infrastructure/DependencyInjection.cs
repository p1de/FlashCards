using FlashCards.Core.Application.Common.Interfaces.Authentication;
using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Core.Application.Common.Interfaces.Services;
using FlashCards.Infrastructure.Authentication;
using FlashCards.Infrastructure.Common.Interfaces;
using FlashCards.Infrastructure.Common.Persistance;
using FlashCards.Infrastructure.Common.Persistance.Interfaces;
using FlashCards.Infrastructure.Common.Persistance.MongoDB;
using FlashCards.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IAppSettings, AppSettingsMongoDb>();
            services.AddSingleton<IDbContext, MongoDbContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}