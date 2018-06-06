using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class TerraceStore : BaseStore<Terrace>, ITerraceStore
    {
        public override string Identifier => "Terrace";
    }
}
