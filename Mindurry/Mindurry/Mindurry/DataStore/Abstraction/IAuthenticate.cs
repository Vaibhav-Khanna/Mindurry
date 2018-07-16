using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.DataStore.Abstraction
{
    public interface IAuthenticate
    {
        Task<bool> LoginAsync(bool useSilent = false);

        Task<bool> LogoutAsync();

    }
}
