using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class UserStore : BaseStore<User>, IUserStore
    {
        public override string Identifier => "User";

    }
}
