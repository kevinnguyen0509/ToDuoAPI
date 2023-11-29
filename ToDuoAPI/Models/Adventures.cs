using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDuoAPI.Models
{
    public class Adventures
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string WebsiteURL { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        public int City { get; set; }

        [Required]
        public int ToDuoStatesID { get; set; }

        [MaxLength(300)]
        public string Tags { get; set; }

        public string Hours { get; set; }

        public int SwipeCount { get; set; }

        // Foreign key
        [Required]
        public int ToDuoCategoryId { get; set; }

        // Navigation property
        [ForeignKey("ToDuoCategoryId")]
        public virtual ToDuoCategory ToDuoCategory { get; set; }

        [ForeignKey("ToDuoStatesID")]
        public virtual ToDuoStates ToDuoStates { get; set; }

        [ForeignKey("City")]
        public virtual ToDuoCity ToDuoCity { get; set; }
    }
}
