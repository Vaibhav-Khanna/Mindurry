using FreshMvvm;

using Microsoft.WindowsAzure.MobileServices;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Abstraction.Stores;


namespace Mindurry.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {

        IApartmentStore apartmentStore;
        public IApartmentStore ApartmentStore => apartmentStore ?? (apartmentStore = FreshIOC.Container.Resolve<IApartmentStore>());

        ICellarStore cellarStore;
        public ICellarStore CellarStore => cellarStore ?? (cellarStore = FreshIOC.Container.Resolve<ICellarStore>());

        ICollectSourceStore collectSourceStore;
        public ICollectSourceStore CollectSourceStore => collectSourceStore ?? (collectSourceStore = FreshIOC.Container.Resolve<ICollectSourceStore>());

        IContactCustomFieldSourceEntryStore contactCustomFieldSourceEntryStore;
        public IContactCustomFieldSourceEntryStore ContactCustomFieldSourceEntryStore => contactCustomFieldSourceEntryStore ?? (contactCustomFieldSourceEntryStore = FreshIOC.Container.Resolve<IContactCustomFieldSourceEntryStore>());

        IContactCustomFieldSourceStore contactCustomFieldSourceStore;
        public IContactCustomFieldSourceStore ContactCustomFieldSourceStore => contactCustomFieldSourceStore ?? (contactCustomFieldSourceStore = FreshIOC.Container.Resolve<IContactCustomFieldSourceStore>());

        IContactCustomFieldStore contactCustomFieldStore;
        public IContactCustomFieldStore ContactCustomFieldStore => contactCustomFieldStore ?? (contactCustomFieldStore = FreshIOC.Container.Resolve<IContactCustomFieldStore>());

        IContactStore contactStore;
        public IContactStore ContactStore => contactStore ?? (contactStore = FreshIOC.Container.Resolve<IContactStore>());

        IDocumentStore documentStore;
        public IDocumentStore DocumentStore => documentStore ?? (documentStore = FreshIOC.Container.Resolve<IDocumentStore>());

        IGarageStore garageStore;
        public IGarageStore GarageStore => garageStore ?? (garageStore = FreshIOC.Container.Resolve<IGarageStore>());

        IGardenStore gardenStore;
        public IGardenStore GardenStore => gardenStore ?? (gardenStore = FreshIOC.Container.Resolve<IGardenStore>());

        INoteStore noteStore;
        public INoteStore NoteStore => noteStore ?? (noteStore = FreshIOC.Container.Resolve<INoteStore>());


        IResidenceStore residenceStore;
        public IResidenceStore ResidenceStore => residenceStore ?? (residenceStore = FreshIOC.Container.Resolve<IResidenceStore>());

        ITerraceStore terraceStore;
        public ITerraceStore TerraceStore => terraceStore ?? (terraceStore = FreshIOC.Container.Resolve<ITerraceStore>());

        IUserStore userStore;
        public IUserStore UserStore => userStore ?? (userStore = FreshIOC.Container.Resolve<IUserStore>());

        IUserFavoriteStore userFavoriteStore;
        public IUserFavoriteStore UserFavoriteStore => userFavoriteStore ?? (userFavoriteStore = FreshIOC.Container.Resolve<IUserFavoriteStore>());


    }
}
