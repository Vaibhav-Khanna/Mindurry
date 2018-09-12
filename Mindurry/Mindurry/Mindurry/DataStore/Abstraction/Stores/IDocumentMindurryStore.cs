using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IDocumentMindurryStore : IBaseStore<DocumentMindurry>
    {
        Task<DocumentMindurry> UploadDocument(byte[] data, DocumentMindurry document);
        Task<IEnumerable<DocumentMindurry>> GetItemsByKindAndReferenceIdAsync(string id, string kind);
        Task<IEnumerable<DocumentMindurry>> GetPostDocumentsByContactId(string id);
        Task<bool> IsValidDocumentName(string DocumentName);
        Task<IEnumerable<bool>> PullLatest(string KindId, string Kind);
    }
}
