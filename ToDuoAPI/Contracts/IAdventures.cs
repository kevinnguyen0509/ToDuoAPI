using System.Threading.Tasks;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Contracts
{
    public interface IAdventures : IGenericRepository<Adventures>
    {
        Task<List<AdventureDto>> GetAdventures(int limitBy);
        Task<bool> Exists(string address);
        Task<IEnumerable<Adventures>> FilteredSearch(FilterDto filterDto, int limitBy);
    }
}
