using Mindurry.Models.DataObjects;
using Mindurry.DataStore.Abstraction.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class ResidenceStore : BaseStore<Residence>, IResidenceStore
    {
        public override string Identifier => "Residence";

        public async Task<IEnumerable<Residence>> GetAllActiveResidences()
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            List<Residence> residences = new List<Residence>();

            try
            {
                var items = await Table.Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

                if (items != null && items.Any())
                {
                    residences.AddRange(items);

                    var Total_count = (items as MobileServiceCollection<Residence>).TotalCount;

                    for (int i = 0; i < Total_count/50; i++)
                    {
                        var _items = await Table.Skip(residences.Count).Take(50).ToEnumerableAsync().ConfigureAwait(false);

                        if(_items!=null && _items.Any())
                        {
                            residences.AddRange(_items);
                        }
                    }

                    residences = residences.Where(r => !r.Archived).ToList();

                    return residences;
                }
            }
            catch (Exception)
            {
            }
            residences = residences.Where(r => !r.Archived).ToList();
            return residences;
        }
    }
}
