using System.ComponentModel.DataAnnotations;

namespace ToDuoAPI.Models
{
    public class ToDuoLikedAdventures
    {
        public int Id { get; set; }

        [Required]
        public int AdventureID { get; set; }

        [Required]
        public int OwnerID { get; set; }
    }
}
