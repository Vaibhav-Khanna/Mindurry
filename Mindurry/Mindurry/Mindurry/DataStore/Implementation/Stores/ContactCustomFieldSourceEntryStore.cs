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

        public async Task<IEnumerable<ContactCustomFieldSourceEntry>> GetItemsByContactCustomFieldSourceName(string contactCustomFieldSourceId)
        {
            InitializeStore();

            try
            {
                return await Table.Where(x => x.ContactCustomFieldSourceId == contactCustomFieldSourceId).OrderBy(x => x.DisplayOrder).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}