using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class DocumentMindurryStore : BaseStore<DocumentMindurry>, IDocumentMindurryStore
    {
        public override string Identifier => "Document";

        public async Task<DocumentMindurry> UploadDocument(byte[] data, DocumentMindurry document)
        {
            try
            {
                HttpClient client = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(data);

                client.DefaultRequestHeaders.Add("X-ZUMO-AUTH", StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken);
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                content.Add(baContent, "uploadedFile", document.Path);
                content.Add(new StringContent(document.ReferenceId), "referenceId");
                content.Add(new StringContent(document.ReferenceKind), "referenceKind");
                content.Add(new StringContent(document.InternalName), "internalName");
                content.Add(new StringContent(document.Name), "name");
                content.Add(new StringContent(document.DocumentType), "documentType");

                DateTime T = System.DateTime.UtcNow;

                var response = await client.PostAsync(Constants.ApplicationURL + "/api/documentMindurry/upload", content);

                TimeSpan TT = System.DateTime.UtcNow - T;

                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    var _document = JsonConvert.DeserializeObject<DocumentMindurry>(resp);
                    return _document;
                }
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<DocumentMindurry>> GetItemsByKindAndReferenceIdAsync(string id, string kind)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            try
            {
                var items = await Table.Where(x => x.ReferenceKind == kind && x.ReferenceId == id).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

                return items;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<IEnumerable<DocumentMindurry>> GetPostDocumentsByContactId(string id)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            try
            {
                var items = await Table.Where(x => x.ReferenceKind == ReferenceKind.customer.ToString() && x.ReferenceId == id).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

                return items;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> IsValidDocumentName(string DocumentName)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            {
                var items = await Table.Where(x => x.Name == DocumentName).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (items != null && items.Any())
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}
