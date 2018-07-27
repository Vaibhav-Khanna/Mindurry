using Mindurry.Models.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IContactStore : IBaseStore<Contact>
    {
        Task<IEnumerable<Contact>> GetItemsByFilterAsync(string Filter, string SortName, bool SortValue, bool forceRefresh = false);
    }
}
