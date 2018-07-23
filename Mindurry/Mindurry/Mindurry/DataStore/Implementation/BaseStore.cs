using FreshMvvm;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Mindurry.DataStore.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mindurry.DataStore.Implementation
{
    public class BaseStore<T> : IBaseStore<T> where T : class, Models.DataObjects.IBaseDataObject, new()
    {
        private IStoreManager storeManager;

        public virtual string Identifier => "Items";

        IMobileServiceTable<T> onlinetable;
        protected IMobileServiceTable<T> OnlineTable
        {
            get { return onlinetable ?? (onlinetable = StoreManager.MobileService.GetTable<T>()); }

        }


        IMobileServiceSyncTable<T> table;
        protected IMobileServiceSyncTable<T> Table
        {
            get { return table ?? (table = StoreManager.MobileService.GetSyncTable<T>()); }
        }

        public async Task DropTable()
        {
            await Table.PurgeAsync();
            table = null;
        }


        public async Task InitializeStore()
        {
            if (storeManager == null)
                storeManager = FreshIOC.Container.Resolve<IStoreManager>();

            if (!storeManager.IsInitialized)
                await storeManager.InitializeAsync().ConfigureAwait(false);
        }


        public virtual async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false, bool AllItems = false)
        {
            await InitializeStore().ConfigureAwait(false);
            if (forceRefresh)
                await PullLatestAsync().ConfigureAwait(false);

            if (AllItems)
                return await Table.IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            else
                return await Table.Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }


        public virtual async Task<IEnumerable<T>> GetNextItemsAsync(int currentitemCount)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                return await Table.Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<T> GetItemAsync(string id)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);
            var items = await Table.Where(s => s.Id == id).ToListAsync().ConfigureAwait(false);

            if (items == null || items.Count == 0)
                return null;

            return items[0];
        }

        public virtual async Task<bool> InsertAsync(T item)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);
            await Table.InsertAsync(item).ConfigureAwait(false);
            await SyncAsync().ConfigureAwait(false);
            return true;
        }

        public virtual async Task<bool> RemoveAsync(T item)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);
            await Table.DeleteAsync(item).ConfigureAwait(false);
            await SyncAsync().ConfigureAwait(false);
            return true;
        }


        public virtual async Task<bool> UpdateAsync(T item)
        {
            await InitializeStore().ConfigureAwait(false);
            await Table.UpdateAsync(item).ConfigureAwait(false);
            await SyncAsync().ConfigureAwait(false);
            return true;
        }

        public virtual async Task<bool> SyncAsync()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                Debug.WriteLine("Unable to sync items, we are offline");
                return false;
            }

            try
            {
                await StoreManager.MobileService.SyncContext.PushAsync(new CancellationToken(false)).ConfigureAwait(false);
                if (!(await PullLatestAsync().ConfigureAwait(false)))
                    return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync items, we have offline capabilities: " + ex);
                return false;
            }

            return true;
        }

        public async Task<bool> PullLatestAsync()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)              
            {
                Debug.WriteLine("Unable to pull items, we are offline");
                return false;
            }

            try
            {
                var query = Table.CreateQuery();
                await Table.PullAsync<T>($"all{Identifier}", query, false, new CancellationToken(false), new PullOptions() { MaxPageSize = 150 }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to pull items, we have offline capabilities: " + ex);
                return false;
            }

            return true;
        }


    }
}

