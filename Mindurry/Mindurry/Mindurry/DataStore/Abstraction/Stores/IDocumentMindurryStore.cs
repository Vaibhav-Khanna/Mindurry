using Mindurry.Models.DataObjects;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IDocumentMindurryStore : IBaseStore<DocumentMindurry>
    {
        Task<DocumentMindurry> UploadDocument(byte[] data, DocumentMindurry document);
    }
}
