using Mindurry.DataStore.Abstraction.Stores;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction
{
    public interface IStoreManager
    {

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
    }
}
