using Mindurry.DataStore.Implementation.Stores;
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
        Task<IEnumerable<Note>> GetRemindersToDoAsync(string userId);
        Task<IEnumerable<Note>> GetRemindersDoneAsync(string userId);
        Task<IEnumerable<PicsStats>> GetContactStat(Qualification typeContact, DateTime dateDeb, DateTime dateFin);
        Task<IEnumerable<Note>> GetSourcesStat(Qualification typeContact, DateTime dateDeb, DateTime dateFin);
    }
}
