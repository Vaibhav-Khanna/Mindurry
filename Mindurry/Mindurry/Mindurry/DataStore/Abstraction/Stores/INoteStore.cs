using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface INoteStore : IBaseStore<Note>
    {
        Task<DateTimeOffset?> GetLastNoteDateAsync(string contactId);
        Task<DateTimeOffset?> GetNextNoteReminderDateAsync(string contactId);
        Task<IEnumerable<Note>> GetNextRemindersByContactIdAsync(string contactId);
        Task<IEnumerable<Note>> GetNotesByContactIdAsync(string contactId);
        Task<IEnumerable<Note>> GetRemindersToDoAsync();
        Task<IEnumerable<Note>> GetRemindersDoneAsync();
    }
}
