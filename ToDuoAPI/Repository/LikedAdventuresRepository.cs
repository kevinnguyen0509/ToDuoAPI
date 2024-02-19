using Microsoft.EntityFrameworkCore;
using ToDuoAPI.Contracts;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;
using ToDuoAPI.Service.StoredProcedures;

namespace ToDuoAPI.Repository
{
    public class LikedAdventuresRepository : GenericRepository<ToDuoLikedAdventures>, ILikedAdventures
    {
        private readonly ToDuoDbContext _context;
        private readonly AdventuresStoredProcedures _adventureStoredProcedure;
        public LikedAdventuresRepository(ToDuoDbContext context, AdventuresStoredProcedures adventuresStoredProcedures) : base(context)
        {
            _context = context;
            _adventureStoredProcedure = adventuresStoredProcedures;
        }

        public async Task<List<MatchUserDTO>> CheckAdventureMatches(int adventureId, ToDuoBasicUsersDTO basicUsersDTO)
        {
            List<MatchUserDTO> result = new List<MatchUserDTO>();
            bool hasFriends = checkIfUserHasFriends(basicUsersDTO);
            if (hasFriends)
            {
                result = await _adventureStoredProcedure.CheckAdventureMatches(adventureId, basicUsersDTO.Id);
                return result;
            }

            return result; //Empty List
        }

        public async Task<bool> Exists(int adventureId, int ownerId)
        {
            ToDuoLikedAdventures likeAdventure = await _context.ToDuoLikedAdventures.FirstOrDefaultAsync(db => db.AdventureID == adventureId && db.OwnerID == ownerId);
            if (likeAdventure != null)
            {
                return true;
            }
            
            return false;

        }

        private bool checkIfUserHasFriends(ToDuoBasicUsersDTO basicUsersDTO)
        {
            if (!string.IsNullOrEmpty(basicUsersDTO.PartnerId)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendOne)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendTwo)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendThree)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendFour)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendFive)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendSix)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendSeven)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendEight)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendNine)
                || !string.IsNullOrEmpty(basicUsersDTO.FriendTen))
            {
                return true;
            }
            return false;
        }
    }
}
