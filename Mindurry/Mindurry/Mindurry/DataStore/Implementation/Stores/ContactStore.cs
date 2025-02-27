﻿using FreshMvvm;
using Microsoft.WindowsAzure.MobileServices;
using Mindurry.DataModels;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class ContactStore : BaseStore<Contact>, IContactStore
    {
        public override string Identifier => "Contact";


        public async Task<IEnumerable<Contact>> GetItemsByTypeAsync(string ContactType, string Filter = null, bool forceRefresh = false) {

            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh) {  await PullLatestAsync().ConfigureAwait(false); }
            if (!String.IsNullOrEmpty(Filter))
            {
                return await Table.Where(x => (((x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter))) && (x.Qualification == ContactType))).OrderBy(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                
            }
            else
            {
               return await Table.Where(x => (x.Qualification == ContactType)).OrderByDescending(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
              }

        }

        public async Task<IEnumerable<Contact>> GetItemsFilterAsync(string Filter = null, bool forceRefresh = false)
        {

            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh) { await PullLatestAsync().ConfigureAwait(false); }
            if (!String.IsNullOrEmpty(Filter))
            {
                return await Table.Where(x => ((x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter)))).OrderBy(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            }
            else
            {
                return await Table.OrderByDescending(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            }

        }

        public async Task<IEnumerable<Contact>> GetNextItemsFilterAsync(int currentitemCount, string Filter = null, bool forceRefresh = false)
        {

            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh) { await PullLatestAsync().ConfigureAwait(false); }
            if (!String.IsNullOrEmpty(Filter))
            {
                return await Table.Where(x => ((x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter)))).OrderBy(x => x.Lastname).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            }
            else
            {
                return await Table.OrderByDescending(x => x.Lastname).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            }

        }

        public async Task<IEnumerable<Contact>> GetNextItemsByTypeAsync(int currentitemCount, string ContactType, string Filter = null, bool forceRefresh = false)
        {

            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh) { await PullLatestAsync().ConfigureAwait(false); }
            if (!String.IsNullOrEmpty(Filter))
            {
                return await Table.Where(x => (((x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter))) && (x.Qualification == ContactType))).OrderBy(x => x.Lastname).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
 
            }
            else
            {
                return await Table.Where(x => (x.Qualification == ContactType)).OrderByDescending(x => x.ContactCreatedAt).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            }

        }

        /* public async Task<IEnumerable<Contact>> GetItemsByFilterAsync(string Filter, string SortName, bool SortValue, bool forceRefresh = false)
         {
             await InitializeStore().ConfigureAwait(false);

             if (forceRefresh)
                 await PullLatestAsync().ConfigureAwait(false);

             if ((Filter != null) && (SortName == null))
             {
                 return await Table.Where(x => (x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter))).OrderBy(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
             }
             if ((Filter != null) && (SortName == "CreatedDate"))
             {
                 if (SortValue == true)
                 {
                     return await Table.Where(x => (x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter))).OrderBy(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                 }
                 else
                 {
                     return await Table.Where(x => (x.Firstname.ToLower().Contains(Filter)) || (x.Lastname.ToLower().Contains(Filter)) || (x.Email.ToLower().Contains(Filter)) || (x.Phone.ToLower().Contains(Filter))).OrderByDescending(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                 }
             }
             if ((Filter == null) && (SortName == "CreatedDate"))
             {
                 if (SortValue == true)
                 {
                     return await Table.OrderBy(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                 }
                 else
                 {
                     return await Table.OrderByDescending(x => x.ContactCreatedAt).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                 }
             }
             else
             {
                 return await Table.OrderBy(x => x.Lastname).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
             }
         } */

        public async Task<JObjectManualQuery> GetItemsByCommercialFilterAsync(string ContactType, List<CheckBoxItem> SelectedCommercials = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
            { await PullLatestAsync().ConfigureAwait(false); }

            try
            {
                //construction dynamique de la query
                string query = "$filter= (qualification eq '" + ContactType + "')";
                //remise objets à null si objet != null mais vide
                if (SelectedCommercials != null && SelectedCommercials.Count()==0) { SelectedCommercials = null; }
                if (SelectedResidences != null && SelectedResidences.Count() == 0) { SelectedResidences = null; }

                if ((SelectedCommercials != null && SelectedCommercials.Any()) && SelectedResidences==null)
                {
                    for (int i = 0; i < SelectedCommercials.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and (userId eq '" + SelectedCommercials[i].Id.ToString() + "'";
                        }
                       else
                        {
                            query += " or userId eq '" + SelectedCommercials[i].Id.ToString() + "'";
                        }
               
                    }
                    query += ")";
                }
                if (SelectedCommercials == null && (SelectedResidences != null && SelectedResidences.Any()))
                {
                    for (int i = 0; i < SelectedResidences.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and (substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                    }
                    query += ")";
                }

                if ((SelectedCommercials != null && SelectedCommercials.Any()) && (SelectedResidences != null && SelectedResidences.Any()))
                {
                    for (int i = 0; i < SelectedCommercials.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and ((userId eq '" + SelectedCommercials[i].Id.ToString() + "'";
                        }
                        else
                        {
                            query += " or userId eq '" + SelectedCommercials[i].Id.ToString() + "'";
                        }
                    }
                    query += ") and ";
                    for (int i = 0; i < SelectedResidences.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "(substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        query += ")";
                    }
                    query += ")";     
                }
                query += "&$top=50&$inlinecount=allpages";
                JToken contactList = await Table.ReadAsync(query);
               
                var result = contactList.ToObject<JObjectManualQuery>();

                return result;

            }
            catch (Exception e)
            {
                return null;
                Debug.WriteLine(e);
            }
           
        }

            public async Task<JObjectManualQuery> GetNextItemsByCommercialFilterAsync(int currentitemCount,string ContactType, List<CheckBoxItem> SelectedCommercials = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
            { await PullLatestAsync().ConfigureAwait(false); }

            try
            {
                //construction dynamique de la query
                string query = "$filter= (qualification eq '" + ContactType + "')";
                //remise objets à null si objet != null mais vide
                if (SelectedCommercials != null && SelectedCommercials.Count() == 0) { SelectedCommercials = null; }
                if (SelectedResidences != null && SelectedResidences.Count() == 0) { SelectedResidences = null; }

                if ((SelectedCommercials != null && SelectedCommercials.Any()) && SelectedResidences == null)
                {
                    for (int i = 0; i < SelectedCommercials.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and (userId eq '" + SelectedCommercials[i].Id.ToString() + "'";
                        }
                        else
                        {
                            query += " or userId eq '" + SelectedCommercials[i].Id.ToString() + "'";
                        }

                    }
                    query += ")";
                }
                if (SelectedCommercials == null && (SelectedResidences != null && SelectedResidences.Any()))
                {
                    for (int i = 0; i < SelectedResidences.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and (substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                    }
                    query += ")";
                }

                if ((SelectedCommercials != null && SelectedCommercials.Any()) && (SelectedResidences != null && SelectedResidences.Any()))
                {
                    for (int i = 0; i < SelectedCommercials.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and ((userId eq '" + SelectedCommercials[i].Id.ToString() + "'";
                        }
                        else
                        {
                            query += " or userId eq '" + SelectedCommercials[i].Id.ToString() + "'";
                        }
                    }
                    query += ") and ";
                    for (int i = 0; i < SelectedResidences.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "(substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        query += ")";
                    }
                    query += ")";
                }
                query += "&$skip(" + currentitemCount + ")&$top=50&$inlinecount=allpages";
                JToken contactList = await Table.ReadAsync(query);
               
                var result = contactList.ToObject<JObjectManualQuery>();

                return result;

            }
            catch (Exception e)
            {
                return null;
                Debug.WriteLine(e);
            }

        }

        public async Task<JObjectManualQuery> GetItemsByTypeFilterAsync(string ContactType, List<CheckBoxItem> SelectedTypes = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
            { await PullLatestAsync().ConfigureAwait(false); }

            try
            {
                //construction dynamique de la query
                string query = "$filter= (qualification eq '" + ContactType + "')";
                //remise objets à null si objet != null mais vide
                if (SelectedTypes != null && SelectedTypes.Count() == 0) { SelectedTypes = null; }
                if (SelectedResidences != null && SelectedResidences.Count() == 0) { SelectedResidences = null; }

                if ((SelectedTypes != null && SelectedTypes.Any()) && SelectedResidences == null)
                {
                    for (int i = 0; i < SelectedTypes.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and (substringof('" + SelectedTypes[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedTypes[i].Id.ToString() + "', CustomFields) eq true";
                        }

                    }
                    query += ")";
                }
                if (SelectedTypes == null && (SelectedResidences != null && SelectedResidences.Any()))
                {
                    for (int i = 0; i < SelectedResidences.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and (substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                    }
                    query += ")";
                }

                if ((SelectedTypes != null && SelectedTypes.Any()) && (SelectedResidences != null && SelectedResidences.Any()))
                {
                    for (int i = 0; i < SelectedTypes.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and ((substringof('" + SelectedTypes[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedTypes[i].Id.ToString() + "', CustomFields) eq true";
                        }
                    }
                    query += ") and ";
                    for (int i = 0; i < SelectedResidences.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "(substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        query += ")";
                    }
                    query += ")";
                }
                query += "&$top=50&$inlinecount=allpages";
                JToken contactList = await Table.ReadAsync(query);

                var result = contactList.ToObject<JObjectManualQuery>();

                return result;

            }
            catch (Exception e)
            {
                return null;
                Debug.WriteLine(e);
            }

        }

        public async Task<JObjectManualQuery> GetNextItemsByTypeFilterAsync(int currentitemCount, string ContactType, List<CheckBoxItem> SelectedTypes = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);

            if (forceRefresh)
            { await PullLatestAsync().ConfigureAwait(false); }

            try
            {
                //construction dynamique de la query
                string query = "$filter= (qualification eq '" + ContactType + "')";
                //remise objets à null si objet != null mais vide
                if (SelectedTypes != null && SelectedTypes.Count() == 0) { SelectedTypes = null; }
                if (SelectedResidences != null && SelectedResidences.Count() == 0) { SelectedResidences = null; }

                if ((SelectedTypes != null && SelectedTypes.Any()) && SelectedResidences == null)
                {
                    for (int i = 0; i < SelectedTypes.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and (substringof('" + SelectedTypes[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedTypes[i].Id.ToString() + "', CustomFields) eq true";
                        }

                    }
                    query += ")";
                }
                if (SelectedTypes == null && (SelectedResidences != null && SelectedResidences.Any()))
                {
                    for (int i = 0; i < SelectedResidences.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and (substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                    }
                    query += ")";
                }

                if ((SelectedTypes != null && SelectedTypes.Any()) && (SelectedResidences != null && SelectedResidences.Any()))
                {
                    for (int i = 0; i < SelectedTypes.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "and ((substringof('" + SelectedTypes[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedTypes[i].Id.ToString() + "', CustomFields) eq true";
                        }
                    }
                    query += ") and ";
                    for (int i = 0; i < SelectedResidences.Count(); i++)
                    {
                        if (i == 0)
                        {
                            query += "(substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        else
                        {
                            query += " or substringof('" + SelectedResidences[i].Id.ToString() + "', CustomFields) eq true";
                        }
                        query += ")";
                    }
                    query += ")";
                }
                query += "&$skip(" + currentitemCount + ")&$top=50&$inlinecount=allpages";
                JToken contactList = await Table.ReadAsync(query);

                var result = contactList.ToObject<JObjectManualQuery>();

                return result;

            }
            catch (Exception e)
            {
                return null;
                Debug.WriteLine(e);
            }

        }

        public async Task<string> RewriteCustomFields(string contactId)
        {
            await InitializeStore().ConfigureAwait(false);

            var storeManager = FreshIOC.Container.Resolve<IStoreManager>();

            var customsFields = (await storeManager.ContactCustomFieldStore.GetItemsByContactIdAsync(contactId)).ToList();
            if (customsFields != null && customsFields.Any())
            {
                string customs = "";
                for (int i = 0; i < customsFields.Count(); i++)
                {
                    var contactCustomFieldSource = await storeManager.ContactCustomFieldSourceStore.GetItemAsync(customsFields[i].ContactCustomFieldSourceId);

                    customs += contactCustomFieldSource.InternalName + "=" + customsFields[i].ContactCustomFieldSourceEntryId + ",";
                }
                
                return customs;
            }
            else return null;

        }
        public async Task<long> GetTotalCountByCollectSourceId (string collectSourceId)
        {
            await InitializeStore().ConfigureAwait(false);
            try
            { //
                var result = await Table.Where(x => (x.CollectSourceId == collectSourceId)).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
                if (result != null && result.Any())
                {
                    return (result as IQueryResultEnumerable<Contact>).TotalCount;
                }
                else return 0;
            }
            catch (Exception)
            {
                return 0;
            }

        }

    }

    public class JObjectManualQuery
    {
        public IEnumerable<Contact> results { get; set; }
        public double count { get; set; }
    }
}