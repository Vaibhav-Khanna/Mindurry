using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IContactCustomFieldSourceEntryStore : IBaseStore<ContactCustomFieldSourceEntry>
    {
        Task<IEnumerable<ContactCustomFieldSourceEntry>> GetItemsByContactCustomFieldSourceName(string contactCustomFieldSourceInternalName);
    }
}
