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
        Task<IEnumerable<ContactCustomField>> GetItemsByContactCustomFieldSourceNameAndContactIdAsync(string contactCustomFieldSourceInternalName, string contactId);
        Task<ContactCustomField> GetItemByContactIdAndSourceIdAsync(string ContactId, string ContactCustomFieldSourceId);
        Task<IEnumerable<ContactCustomField>> GetItemsByContactIdAsync(string ContactId);
        Task<ContactCustomField> GetItemByContactIdAndSourceEntryIdAsync(string ContactId, string ContactCustomFieldSourceEntryId);
        Task<long> GetTotalCountByContactCustomFieldSourceEntryId(string ContactCustomFieldSourceEntryId, DateTime dateDebut, DateTime dateFin);
        }
}
