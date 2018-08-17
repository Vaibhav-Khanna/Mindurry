using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class TerraceStore : BaseStore<Terrace>, ITerraceStore
    {
        public override string Identifier => "Terrace";

        public async Task<IEnumerable<Terrace>> GetTerracesByResidenceId(string residenceId,string apartmentId)
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);

                var items = await Table.Where(t => t.ResidenceId == residenceId && t.ApartmentId == apartmentId).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

                return items;
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
