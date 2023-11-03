using FlashCards.Domain.Entities.Interfaces;

namespace FlashCards.Core.Application.Common.Interfaces.Persistance
{
    public interface IOfflineGenericRepository<TCollection> : IGenericRepository<TCollection> where TCollection : class, IIdentity, new()
    {
    }
}