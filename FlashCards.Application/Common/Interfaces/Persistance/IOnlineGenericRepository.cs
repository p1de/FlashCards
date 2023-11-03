namespace FlashCards.Core.Application.Common.Interfaces.Persistance
{
    public interface IOnlineGenericRepository<TCollection> : IGenericRepository<TCollection> where TCollection : class, new()
    {
    }
}