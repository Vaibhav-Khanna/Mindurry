using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class GarageStore : BaseStore<Garage>, IGarageStore
    {
        public override string Identifier => "Garage";

        public async Task<IEnumerable<Garage>> GetItemsByResidenceId(string residenceId)
        {

            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);


            if (!String.IsNullOrEmpty(residenceId))
            {
                return await Table.Where(x => x.ResidenceId == residenceId).OrderBy(x => x.LotNumberArchitect).ToEnumerableAsync().ConfigureAwait(false);
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> IsStillGarageInResidence(string residenceId)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            if (!String.IsNullOrEmpty(residenceId))
            {
                IEnumerable<Garage> garages = await Table.Where(x => x.ResidenceId == residenceId).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (garages != null && garages.Any())
                {
                    var garage = garages.ToList().Where(g => String.IsNullOrEmpty(g.ContactId));
                    if (garage != null && garage.Count() > 0 )
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Garage>> GetItemsByContactId(string contactId)
        {

            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);


            if (!String.IsNullOrEmpty(contactId))
            {
                return await Table.Where(x => x.ContactId == contactId).OrderBy(x => x.ResidenceId).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else
            {
                return null;
            }
        }
        public async Task<Garage> GetItemByRefenceAsync(string reference)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);


            if (!String.IsNullOrEmpty(reference))
            {
                var items = await Table.Where(x => x.LotNumberArchitect == reference).ToListAsync().ConfigureAwait(false);

                if (items == null || items.Count == 0)
                    return null;

                return items[0];

            }
            else
            {
                return null;
            }
        }
    }
}
