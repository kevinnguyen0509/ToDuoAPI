using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Contracts
{
    public interface ILikedAdventures : IGenericRepository<ToDuoLikedAdventures>
    {
        Task<bool> Exists (int adventureId, int ownerId);
        Task<List<MatchUserDTO>> CheckAdventureMatches(int adventureId, ToDuoBasicUsersDTO basicUsersDTO);
    }
}
