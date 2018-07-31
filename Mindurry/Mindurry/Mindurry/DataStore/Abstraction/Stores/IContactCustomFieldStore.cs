using Mindurry.DataModels;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IContactCustomFieldStore : IBaseStore<ContactCustomField>
    {
        Task<IEnumerable<ContactCustomField>> GetItemsByContactCustomFieldSourceName(string contactCustomFieldSourceInternalName, string contactId);
    }
}
