using FreshMvvm;

using Microsoft.WindowsAzure.MobileServices;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Abstraction.Stores;


namespace Mindurry.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {
        public static MobileServiceClient MobileService { get; set; }

        IApartmentStore apartmentStore;
        public IApartmentStore ApartmentStore => apartmentStore ?? (apartmentStore = FreshIOC.Container.Resolve<IApartmentStore>());

        ICellarStore cellarStore;
        public ICellarStore CellarStore => cellarStore ?? (cellarStore = FreshIOC.Container.Resolve<ICellarStore>());

        IContactStore contactStore;
        public IContactStore ContactStore => contactStore ?? (contactStore = FreshIOC.Container.Resolve<IContactStore>());

        IDocumentStore documentStore;
        public IDocumentStore DocumentStore => documentStore ?? (documentStore = FreshIOC.Container.Resolve<IDocumentStore>());

        IGarageStore garageStore;
        public IGarageStore GarageStore => garageStore ?? (garageStore = FreshIOC.Container.Resolve<IGarageStore>());

        IGardenStore gardenStore;
        public IGardenStore GardenStore => gardenStore ?? (gardenStore = FreshIOC.Container.Resolve<IGardenStore>());

        IResidenceStore residenceStore;
        public IResidenceStore ResidenceStore => residenceStore ?? (residenceStore = FreshIOC.Container.Resolve<IResidenceStore>());

        ITerraceStore terraceStore;
        public ITerraceStore TerraceStore => terraceStore ?? (terraceStore = FreshIOC.Container.Resolve<ITerraceStore>());

        IUserStore userStore;
        public IUserStore UserStore => userStore ?? (userStore = FreshIOC.Container.Resolve<IUserStore>());

        IUserFavoriteStore userFavoriteStore;
        public IUserFavoriteStore UserFavoriteStore => userFavoriteStore ?? (userFavoriteStore = FreshIOC.Container.Resolve<IUserFavoriteStore>());


        
        

        public MobileServiceClient CurrentClient
        {
            get {
                MobileService = new MobileServiceClient(Constants.ApplicationURL);
                return MobileService; }
        }
        

    }
}
