using Mindurry.Models.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IGarageStore : IBaseStore<Garage>
    {
        Task<IEnumerable<Garage>> GetItemsByResidenceId(string residenceId);
        Task<IEnumerable<Garage>> GetItemsByContactId(string contactId);
        Task<Garage> GetItemByRefenceAsync(string reference);
    }
}
