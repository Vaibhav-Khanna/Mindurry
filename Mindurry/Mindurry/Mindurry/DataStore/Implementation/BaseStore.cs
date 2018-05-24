using FreshMvvm;
using Microsoft.WindowsAzure.MobileServices;
using Mindurry.DataStore.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation
{
    public class BaseStore<T> : IBaseStore<T> where T : class, Models.DataObjects.IBaseDataObject, new()
    {

        IStoreManager storeManager;

        public virtual string Identifier => "Items";

        IMobileServiceTable<T> table;
        protected IMobileServiceTable<T> Table
        {
            get { return table ?? (table = StoreManager.MobileService.GetTable<T>()); }

        }

        public void DropTable()
        {
            table = null;
        }


        public void InitializeStore()
        {
            if (storeManager == null)
                storeManager = FreshIOC.Container.Resolve<IStoreManager>();
          
        }


        public virtual async Task<IEnumerable<T>> GetItemsAsync(int currentCount = 0)
        {
            try
            {
                InitializeStore();
                return await Table.Skip(currentCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                var error = await ex.Response?.Content?.ReadAsStringAsync();
                Debug.WriteLine(error);
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual async Task<T> GetItemAsync(string id)
        {
            InitializeStore();

            try
            {
                var item = await Table.LookupAsync(id);

                if (item == null)
                    return null;

                return item;

            }
            catch (Exception)
            {
                return null;
            }

        }

        public virtual async Task<bool> InsertAsync(T item)
        {
            InitializeStore();

            try
            {
                await Table.InsertAsync(item).ConfigureAwait(false);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                var error = await ex.Response?.Content?.ReadAsStringAsync();
                Debug.WriteLine(error);
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public virtual async Task<bool> RemoveAsync(T item)
        {
            InitializeStore();

            try
            {
                await Table.DeleteAsync(item).ConfigureAwait(false);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                var error = await ex.Response?.Content?.ReadAsStringAsync();
                Debug.WriteLine(error);
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {

            InitializeStore();

            try
            {
                await Table.UpdateAsync(item).ConfigureAwait(false);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                var error = await ex.Response?.Content?.ReadAsStringAsync();
                Debug.WriteLine(error);
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }


            return true;
        }


    }
}

