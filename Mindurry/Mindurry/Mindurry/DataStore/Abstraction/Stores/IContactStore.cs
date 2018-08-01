using Mindurry.DataModels;
using Mindurry.Models.DataObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IContactStore : IBaseStore<Contact>
    {
        // Task<IEnumerable<Contact>> GetItemsByFilterAsync(string Filter, string SortName, bool SortValue, bool forceRefresh = false);

        Task<IEnumerable<Contact>> GetItemsByTypeAsync(string ContactType, string Filter = null, bool forceRefresh = false);
        Task<IEnumerable<Contact>> GetItemsByCommercialFilterAsync(string ContactType, List<CheckBoxItem> SelectedCommercials = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false);
        Task<IEnumerable<Contact>> GetItemsByTypeFilterAsync(string ContactType, List<CheckBoxItem> SelectedTypes = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false);
    }
}
