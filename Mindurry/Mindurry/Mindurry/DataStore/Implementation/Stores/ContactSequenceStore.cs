using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class ContactSequenceStore : BaseStore<ContactSequence>, IContactSequenceStore
    {
        public override string Identifier => "ContactSequence";

        public async Task<bool> IsContactSequence(string contactId)
        {
            try
            { 
                 var result = await Table.Where(x => (x.ContactId == contactId) && (x.EndAt == null)).ToEnumerableAsync().ConfigureAwait(false);
                 if (result != null && result.Any())
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public async Task<ContactSequence> SequenceInProgress(string contactId)
        {
            try
            {
                var result = await Table.Where(x => (x.ContactId == contactId) && (x.EndAt == null)).ToEnumerableAsync().ConfigureAwait(false);
                if (result != null && result.Any())
                {
                    return result.First();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
