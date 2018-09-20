using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Helpers;
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
                var items = await Table.Where(x => x.ReferenceKind == ReferenceKind.Customer.ToString().ToLower() && x.ReferenceId == id).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

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
            await PullLatestAsync().ConfigureAwait(false);
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

        private async Task<Byte[]> DownLoadDocument(string id)
        {
            var uri = new Uri($"{Constants.ApplicationURL}/api/documentMindurry/{id}/file?token={StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken}");

            try
            {
                var _client = new HttpClient();

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (stream != null)
                        return StoreManager.ReadFully(stream);
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }


        public async Task<IEnumerable<bool>> PullLatest(string KindId, string Kind)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            var items = await GetItemsByKindAndReferenceIdAsync(KindId, Kind);
            List<bool> returnBool = new List<bool>();
            foreach (var item in items)
            {
                var fileName = item.InternalName + "." + item.Extension;
                var IsExist = await PclStorage.IsFileExistAsync(fileName, item.ReferenceKind, item.ReferenceId);

                if (!IsExist && !string.IsNullOrEmpty(item.Path)) // Only download files who have some value in the path field.
                {
                    var document = await DownLoadDocument(item.Id);

                    if (document != null)
                    {                      
                        var response = await PclStorage.SaveFileLocal(document, fileName, item.ReferenceKind, item.ReferenceId);
                        if (response==true)
                        {
                            returnBool.Add(true);
                        } 
                        else
                        {
                            returnBool.Add(false);
                        }
                    }
                    else
                    {
                         returnBool.Add(false);
                    }
                }
                else
                {
                    returnBool.Add(true);
                }
            }
            return returnBool;
        }

    }
}
