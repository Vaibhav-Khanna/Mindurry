using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class ContactStore : BaseStore<Contact>, IContactStore
    {
        public override string Identifier => "Contact";

        public async Task<IEnumerable<Contact>> GetItemsByFilterAsync(string Filter, string SortName, bool SortValue, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
                await PullLatestAsync().ConfigureAwait(false);

            if ((Filter != null) && (SortName == null))
            {
                return await Table.Where(x => (x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter))).OrderBy(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            if ((Filter != null) && (SortName == "CreatedDate"))
            {
                if (SortValue == true)
                {
                    return await Table.Where(x => (x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter))).OrderBy(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
                else
                {
                    return await Table.Where(x => (x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter))).OrderByDescending(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

                }
            }
            if ((Filter == null) && (SortName == "CreatedDate"))
            {
                if (SortValue == true)
                {
                    return await Table.OrderBy(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                }
                else
                {
                    return await Table.OrderByDescending(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

                }
            }
            else
            {
                return await Table.OrderBy(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            }
        }
    }
}