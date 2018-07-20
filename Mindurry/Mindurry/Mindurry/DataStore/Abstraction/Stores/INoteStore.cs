using Mindurry.Models.DataObjects;
using System;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface INoteStore : IBaseStore<Note>
    {
        Task<DateTimeOffset?> GetLastNoteDateAsync(string contactId);
        Task<DateTimeOffset?> GetNextNoteReminderDateAsync(string contactId);
    }
}
