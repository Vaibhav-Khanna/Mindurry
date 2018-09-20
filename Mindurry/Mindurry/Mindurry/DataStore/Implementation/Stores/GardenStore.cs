using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class GardenStore : BaseStore<Garden>, IGardenStore
    {
        public override string Identifier => "Garden";

        public async Task<IEnumerable<Garden>> GetGardensByResidenceId(string residenceId,string apartmentId)
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);
              //  await PullLatestAsync().ConfigureAwait(false);

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
