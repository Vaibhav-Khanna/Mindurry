using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Implementation.Stores
{
    public class UserSalesTeamStore : BaseStore<UserSalesTeam>, IUserSalesTeamStore
    {
        public override string Identifier => "UserSalesTeam";

        public async Task<string> GetSalesTeamIdAsync(string userId)
        {
            await InitializeStore().ConfigureAwait(false);
            
            if (!String.IsNullOrEmpty(userId))
            {
                var items = await Table.Where(x => x.UserId == userId).ToListAsync().ConfigureAwait(false);

                if (items == null || items.Count == 0)
                    return null;

                return items[0].SalesTeamId;

            }
            else
            {
                return null;
            }
        }

    }
}
