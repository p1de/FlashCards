using FlashCards.Core.Application.Common.Interfaces.Services;

namespace FlashCards.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}