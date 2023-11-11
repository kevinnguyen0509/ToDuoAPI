using System.ComponentModel.DataAnnotations;

namespace ToDuoAPI.Models
{
    public class ToDuoUsers
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [MaxLength(50)]
        public string Salt { get; set; }

        [MaxLength(20)]
        public string PartnerId { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(10)]
        public string FriendOne { get; set; }

        [MaxLength(10)]
        public string FriendTwo { get; set; }

        [MaxLength(10)]
        public string FriendThree { get; set; }

        [MaxLength(10)]
        public string FriendFour { get; set; }

        [MaxLength(10)]
        public string FriendFive { get; set; }

        [MaxLength(10)]
        public string FriendSix { get; set; }

        [MaxLength(10)]
        public string FriendSeven { get; set; }

        [MaxLength(10)]
        public string FriendEight { get; set; }

        [MaxLength(10)]
        public string FriendNine { get; set; }

        [MaxLength(10)]
        public string FriendTen { get; set; }

    }
}
