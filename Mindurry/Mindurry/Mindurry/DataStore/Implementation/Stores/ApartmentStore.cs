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

        public async Task<IEnumerable<Apartment>> GetApartmentsByResidenceId(string residenceId)
        {            
            try
            {
                await InitializeStore().ConfigureAwait(false);

                var items = await Table.Where( a=> a.ResidenceId == residenceId).ToEnumerableAsync().ConfigureAwait(false);

                return items;
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
