using Mindurry.Models.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IApartmentStore : IBaseStore<Apartment>
    {

        Task<IEnumerable<Apartment>> GetApartmentsByResidenceId(string residenceId);
    
        Task<IEnumerable<Apartment>> GetItemsByResidenceId(string residenceId);
        Task<IEnumerable<Apartment>> GetItemsByContactId(string contactId);
        Task<Apartment> GetItemByRefenceAsync(string reference);
    }  
}
