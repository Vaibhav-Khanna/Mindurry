using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction
{
    public interface IBaseStore<T>
    {
        void InitializeStore();
        Task<IEnumerable<T>> GetItemsAsync(int currentCount = 0);
        Task<T> GetItemAsync(string id);
        Task<bool> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> RemoveAsync(T item);

        void DropTable();

        string Identifier { get; }

    }
}
