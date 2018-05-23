using Mindurry.DataStore.Abstraction.Stores;
using System.Threading.Tasks;

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
        ITerraceStore TerraceStore { get; }
        IUserFavoriteStore UserFavoriteStore { get; }
        IUserStore UserStore { get; }

        Task<bool> LoginAsync(bool useSilent = false);

        Task<bool> LogoutAsync();
    }
}
