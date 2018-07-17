using Mindurry.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mindurry.Services.Abstraction
{
    public interface IPlaceService
    {
        Task<WebServiceResults.Result> GetResult(string input);
        Task<WebServiceDetailResults> GetResultDetail(string input);
    }
}
