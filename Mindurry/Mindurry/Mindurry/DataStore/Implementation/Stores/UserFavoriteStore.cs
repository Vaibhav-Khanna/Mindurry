using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
namespace Mindurry.DataStore.Implementation.Stores
{
    public class UserFavoriteStore : BaseStore<UserFavorite>, IUserFavoriteStore
    {
        public override string Identifier => "UserFavorite";
    }
}
