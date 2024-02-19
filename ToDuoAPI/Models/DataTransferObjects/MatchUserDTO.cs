using System.ComponentModel.DataAnnotations;

namespace ToDuoAPI.Models.DataTransferObjects
{
    public class MatchUserDTO
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }
    }
}
