using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
namespace Mindurry.DataStore.Implementation.Stores
{
    public class CellarStore : BaseStore<Cellar>, ICellarStore
    {
        public override string Identifier => "Cellar";
    }
}
