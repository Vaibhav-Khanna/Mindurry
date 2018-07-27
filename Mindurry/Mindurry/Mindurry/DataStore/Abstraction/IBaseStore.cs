using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction
{
    public interface IBaseStore<T>
    {
        Task InitializeStore();
        
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false, bool AllItems = false);
        Task<IEnumerable<T>> GetNextItemsAsync(int currentitemCount);
        Task<bool> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> RemoveAsync(T item);
        Task<bool> SyncAsync();

        Task DropTable();

        string Identifier { get; }

    }

}
