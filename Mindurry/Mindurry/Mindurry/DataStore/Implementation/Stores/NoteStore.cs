using FreshMvvm;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class NoteStore : BaseStore<Note>, INoteStore
    {
        public override string Identifier => "Note";

        public async Task<DateTimeOffset?> GetLastNoteDateAsync(string contactId)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
                var collection = await Table.Where(x => (x.ContactId == contactId)).OrderByDescending(x => x.DatabaseInsertAt).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (collection != null && collection.Any())
                {
                    return collection.First().DatabaseInsertAt;
                } else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DateTimeOffset?> GetNextNoteReminderDateAsync(string contactId)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
                var collection = await Table.Where(x => ((x.ContactId == contactId) && x.ReminderAt != null)).OrderBy(x => x.ReminderAt).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (collection != null && collection.Any())
                {
                    return collection.First().ReminderAt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Note>> GetNextRemindersByContactIdAsync(string contactId)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
                var collection = await Table.Where(x => ((x.ContactId == contactId) && x.ReminderAt != null && x.DoneAt == null)).OrderBy(x => x.ReminderAt).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (collection != null && collection.Any())
                {
                    return collection;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Note>> GetRemindersToDoAsync(string userId)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
                var collection = await Table.Where(x => (x.ReminderAt != null && x.DoneAt == null && x.UserId==userId)).OrderBy(x => x.ReminderAt).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (collection != null && collection.Any())
                {
                    return collection;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Note>> GetRemindersDoneAsync(string userId)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
                var collection = await Table.Where(x => (x.ReminderAt != null && x.DoneAt != null && x.UserId == userId)).OrderBy(x => x.ReminderAt).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (collection != null && collection.Any())
                {
                    return collection;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Note>> GetNotesByContactIdAsync(string contactId)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
                var collection = await Table.Where(x => (x.ContactId == contactId && x.ActivityStreamDate != null && x.Kind != "contactAdded" && x.Kind != "contactUpdated" && x.Kind != "contactDeleted")).OrderByDescending(x => x.ActivityStreamDate).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (collection != null && collection.Any())
                {
                    return collection;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PicsStats>> GetContactStat(Qualification typeContact, DateTime dateDeb, DateTime dateFin)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
               
                var collection = await Table.Where(x => ((x.Extra1 == typeContact.ToString()) && (x.DatabaseInsertAt >= dateDeb) && (x.DatabaseInsertAt < dateFin))).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (collection != null && collection.Any())
                {

                    var tri = collection.GroupBy(x => x.DatabaseInsertAt.Date)
                        .Select(g => new PicsStats
                        {
                            Total = g.Count(),
                            DateStat = g.Key.Date
                        })
                        .OrderBy(g => g.DateStat);
                    
                    return tri; 
                                
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            { 
                return null;
            }
        }

        public async Task<IEnumerable<SourcesStats>> GetSourcesStat(Qualification typeContact, DateTime dateDeb, DateTime dateFin)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {

                var collection = await Table.Where(x => ((x.Extra1 == typeContact.ToString()) && (x.DatabaseInsertAt >= dateDeb) && (x.DatabaseInsertAt < dateFin))).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (collection != null && collection.Any())
                {
                    //List de contact
                    List<Contact> contactList = new List<Contact>();
                    var storeManager = FreshIOC.Container.Resolve<IStoreManager>();
                    foreach (var item in collection)
                    {
                        var contact = await storeManager.ContactStore.GetItemAsync(item.ContactId);
                        contactList.Add(contact);
                    }
                    long totalContact = contactList.Count();
                    var tri = contactList.GroupBy(x => x.CollectSourceName)
                        .Select(g => new SourcesStats
                        {
                            Total = (g.Count() * 100) / totalContact,
                            SourceName = g.Key
                        })
                        .OrderBy(g => g.SourceName);

                    return tri;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    public class PicsStats
    {
        public DateTime DateStat { get; set; }
        public long Total { get; set; }

    }
    public class SourcesStats
    {
        public string SourceName { get; set; }
        public float Total { get; set; }

    }
}
