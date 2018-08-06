using Mindurry.Models.DataObjects;
using Mindurry.DataStore.Abstraction.Stores;
using System;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class ResidenceStore : BaseStore<Residence>, IResidenceStore
    {
        public override string Identifier => "Residence";
    }
}
