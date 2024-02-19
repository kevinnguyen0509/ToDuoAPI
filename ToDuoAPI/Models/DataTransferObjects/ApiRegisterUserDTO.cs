using System.ComponentModel.DataAnnotations;

namespace ToDuoAPI.Models.DataTransferObjects
{
    public class ApiRegisterUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(int.MaxValue, ErrorMessage = "Password must be bigger than 5 characters", MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
    }
}
