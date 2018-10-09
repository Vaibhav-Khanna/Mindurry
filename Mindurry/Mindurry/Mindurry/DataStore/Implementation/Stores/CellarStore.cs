using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class CellarStore : BaseStore<Cellar>, ICellarStore
    {
        public override string Identifier => "Cellar";

        public async Task<IEnumerable<Cellar>> GetItemsByResidenceId(string residenceId)
        {

            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            if (!String.IsNullOrEmpty(residenceId))
            {
                return await Table.Where(x => x.ResidenceId == residenceId).OrderBy(x => x.LotNumberArchitect).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else
            {
                return null;
            }
        }
        public async Task<IEnumerable<Cellar>> GetItemsByContactId(string contactId)
        {

            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            if (!String.IsNullOrEmpty(contactId))
            {
                return await Table.Where(x => x.ContactId == contactId).OrderBy(x => x.ResidenceId).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            else
            {
                return null;
            }
        }
        public async Task<Cellar> GetItemByRefenceAsync(string reference)
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
        public async Task<bool> IsStillCellarInResidence(string residenceId)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            if (!String.IsNullOrEmpty(residenceId))
            {
                IEnumerable<Cellar> cellars = await Table.Where(x => x.ResidenceId == residenceId).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (cellars != null && cellars.Any())
                {
                    var cellar = cellars.ToList().Where(g => String.IsNullOrEmpty(g.ContactId));
                    if (cellar != null && cellar.Count() > 0)
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
    }
}
