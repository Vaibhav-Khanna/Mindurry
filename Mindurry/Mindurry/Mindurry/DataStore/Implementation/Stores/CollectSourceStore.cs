using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class CollectSourceStore : BaseStore<CollectSource>, ICollectSourceStore
    {
        public override string Identifier => "CollectSource";
    }
}
