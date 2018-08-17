using Mindurry.Models.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface ITerraceStore : IBaseStore<Terrace>
    {
        Task<IEnumerable<Terrace>> GetTerracesByResidenceId(string residenceId,string apartmentId);
    }
}
