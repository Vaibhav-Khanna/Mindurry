using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class ContactCustomFieldSourceEntryStore : BaseStore<ContactCustomFieldSourceEntry>, IContactCustomFieldSourceEntryStore
    {
        public override string Identifier => "ContactCustomFieldSourceEntry";

        public async Task<IEnumerable<ContactCustomFieldSourceEntry>> GetItemsByContactCustomFieldSourceName(string contactCustomFieldSourceInternalName)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                return await Table.Where(x => (x.ContactCustomFieldSourceInternalName == contactCustomFieldSourceInternalName) ).OrderBy(x => x.DisplayOrder).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}