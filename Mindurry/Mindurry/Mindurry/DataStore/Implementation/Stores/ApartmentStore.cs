using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class ApartmentStore : BaseStore<Apartment>, IApartmentStore
    {
        public override string Identifier => "Apartment";

        public async  Task<IEnumerable<Apartment>> GetItemsByResidenceId(string residenceId) {

            await InitializeStore().ConfigureAwait(false);

            
            if (!String.IsNullOrEmpty(residenceId))
            {
                return await Table.Where(x => x.ResidenceId == residenceId).OrderBy(x => x.LotNumberArchitect).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false); 
            }
            else
            {
                return null;
            }
        }
        public async Task<IEnumerable<Apartment>> GetItemsByContactId(string contactId)
        {

            await InitializeStore().ConfigureAwait(false);


            if (!String.IsNullOrEmpty(contactId))
            {
                return await Table.Where(x => x.ContactId == contactId).OrderBy(x => x.ResidenceId).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
               
            }
            else
            {
                return null;
            }
        }
        public async Task<Apartment> GetItemByRefenceAsync(string reference)
        {
            await InitializeStore().ConfigureAwait(false);


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
