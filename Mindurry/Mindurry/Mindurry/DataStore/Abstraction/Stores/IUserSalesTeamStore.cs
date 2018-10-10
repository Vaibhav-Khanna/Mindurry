using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IUserSalesTeamStore : IBaseStore<UserSalesTeam>
    {
        Task<string> GetSalesTeamIdAsync(string userId);
    }
}
