namespace FlashCards.Core.Application.Common.Interfaces.Persistance
{
    public interface IGenericRepository<TCollection> where TCollection : class
    {
        Task<bool> AddItemAsync(TCollection item);

        Task<IEnumerable<TCollection>> GetAll();

        Task<TCollection> GetItemByKeyAsync<TFilterType>(string keyName, TFilterType filter) where TFilterType : class;
    }
}