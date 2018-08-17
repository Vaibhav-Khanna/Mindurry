using Mindurry.Models.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IGardenStore : IBaseStore<Garden>
    {
        Task<IEnumerable<Garden>> GetGardensByResidenceId(string residenceId,string apartmentId);
    }
}
