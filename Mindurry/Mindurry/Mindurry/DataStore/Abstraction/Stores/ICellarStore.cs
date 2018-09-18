using Mindurry.Models.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface ICellarStore : IBaseStore<Cellar>
    {
        Task<IEnumerable<Cellar>> GetItemsByResidenceId(string residenceId);
        Task<IEnumerable<Cellar>> GetItemsByContactId(string contactId);
        Task<Cellar> GetItemByRefenceAsync(string reference);
        Task<bool> IsStillCellarInResidence(string residenceId);
    }
}
