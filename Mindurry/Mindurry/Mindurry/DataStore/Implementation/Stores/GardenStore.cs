using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
namespace Mindurry.DataStore.Implementation.Stores
{
    public class GardenStore : BaseStore<Garden>, IGardenStore
    {
        public override string Identifier => "Garden";
    }
}
