namespace FlashCards.Core.Application.Common.Interfaces.Persistance
{
    public interface IOfflineGenericRepository<TCollection> : IGenericRepository<TCollection> where TCollection : class, new()
    {
    }
}