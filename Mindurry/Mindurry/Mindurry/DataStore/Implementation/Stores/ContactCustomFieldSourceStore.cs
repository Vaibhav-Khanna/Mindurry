using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class ContactCustomFieldSourceStore : BaseStore<ContactCustomFieldSource>, IContactCustomFieldSourceStore
    {
        public override string Identifier => "ContactCustomFieldSource";
    }
}
