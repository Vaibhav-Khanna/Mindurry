﻿using Mindurry.DataStore.Abstraction.Stores;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();

        IApartmentStore ApartmentStore { get; }
        ICellarStore CellarStore { get; }
        IContactCustomFieldSourceEntryStore ContactCustomFieldSourceEntryStore { get; }
        IContactCustomFieldSourceStore ContactCustomFieldSourceStore { get; }
        IContactCustomFieldStore ContactCustomFieldStore { get; }
        IContactStore ContactStore { get; }       
        ICollectSourceStore CollectSourceStore { get; }
        IDocumentStore DocumentStore { get; }
        IGarageStore GarageStore { get; }
        IGardenStore GardenStore { get; }
        INoteStore NoteStore { get; }
        IResidenceStore ResidenceStore { get; }
        ITerraceStore TerraceStore { get; }
        IUserFavoriteStore UserFavoriteStore { get; }
        IUserStore UserStore { get; }

        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();

        Task<bool> LoginAsync(bool useSilent = false);

        Task<bool> LogoutAsync();
    }
}
