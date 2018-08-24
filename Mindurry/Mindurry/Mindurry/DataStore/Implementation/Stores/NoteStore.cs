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
        public async Task<IEnumerable<Note>> GetRemindersToDoAsync()
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
                var collection = await Table.Where(x => (x.ReminderAt != null && x.DoneAt == null)).OrderBy(x => x.ReminderAt).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
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

        public async Task<IEnumerable<Note>> GetRemindersDoneAsync()
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
                var collection = await Table.Where(x => (x.ReminderAt != null && x.DoneAt != null)).OrderBy(x => x.ReminderAt).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
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
    }
}
