using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
namespace Mindurry.DataStore.Implementation.Stores
{
    public class DocumentMindurryStore : BaseStore<DocumentMindurry>, IDocumentMindurryStore
    {
        public override string Identifier => "Document";
    }
}
