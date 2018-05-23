using Mindurry.DataStore.Abstraction.Stores;

namespace Mindurry.DataStore.Abstraction
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }

        void Initialize();


        IApartmentStore ApartmentStore { get; }
        ICellarStore CellarStore { get; }
        IContactStore ContactStore { get; }
        IDocumentStore DocumentStore { get; }
        IGarageStore GarageStore { get; }
        IGardenStore GardenStore { get; }
        IResidenceStore ResidenceStore { get; }
        ITerrasseStore TerrasseStore { get; }
        IUserFavoriteStore UserFavoriteStore { get; }
        IUserStore UserStore { get; }


    }
}
