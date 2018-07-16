using Mindurry.Models.DataObjects;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction.Stores
{
    public interface IUserStore : IBaseStore<User>
    {
        Task<User> GetProfileAsync(string token);
    }
}
