using System.ComponentModel.DataAnnotations;

namespace ToDuoAPI.Models.DataTransferObjects
{
    public class MatchUserDTO
    {
        public int AdventureId { get; set; }
        public string Title { get; set; }

        public string ImageURL { get; set; }

        public int LikedAdventureId { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }
    }
}
