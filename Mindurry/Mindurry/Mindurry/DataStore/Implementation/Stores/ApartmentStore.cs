using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
namespace Mindurry.DataStore.Implementation.Stores
{
    public class ApartmentStore : BaseStore<Apartment>, IApartmentStore
    {
        public override string Identifier => "Apartment";

    }
}
