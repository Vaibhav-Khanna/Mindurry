using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
namespace Mindurry.DataStore.Implementation.Stores
{
    public class GarageStore : BaseStore<Garage>, IGarageStore
    {
        public override string Identifier => "Garage";
    }
}
