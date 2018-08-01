﻿using Mindurry.DataModels;
using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class ContactCustomFieldStore : BaseStore<ContactCustomField>, IContactCustomFieldStore
    {
        public override string Identifier => "ContactCustomField";

        public async Task<IEnumerable<ContactCustomField>> GetItemsByContactCustomFieldSourceName(string contactCustomFieldSourceInternalName, string contactId)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                return await Table.Where(x => ((x.ContactCustomFieldSourceInternalName == contactCustomFieldSourceInternalName) && (x.ContactId == contactId))).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ContactCustomField> GetItemByContactIdAndSourceEntryIdAsync(string ContactId, string ContactCustomFieldSourceId)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                 var result = await Table.Where(x => ((x.ContactId == ContactId) && (x.ContactCustomFieldSourceId == ContactCustomFieldSourceId))).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (result != null)
                {
                    return result.ToList()[0];
                }
                else return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<ContactCustomField>> GetItemsByContactIdAsync(string ContactId)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                var result = await Table.Where(x => (x.ContactId == ContactId)).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (result != null)
                {
                    return result;
                }
                else return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
