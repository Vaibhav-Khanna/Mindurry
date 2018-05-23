using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
namespace Mindurry.DataStore.Implementation.Stores
{
    public class DocumentStore : BaseStore<Document>, IDocumentStore
    {
        public override string Identifier => "Document";
    }
}
