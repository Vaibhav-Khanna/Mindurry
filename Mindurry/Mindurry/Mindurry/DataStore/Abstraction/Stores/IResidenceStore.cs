using Mindurry.Models.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IResidenceStore : IBaseStore<Residence>
    {
        Task<IEnumerable<Residence>> GetAllActiveResidences();
    }
}
