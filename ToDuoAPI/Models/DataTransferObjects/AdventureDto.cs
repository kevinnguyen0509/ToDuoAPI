namespace ToDuoAPI.Models.DataTransferObjects
{
    public class AdventureDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string WebsiteURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Tags { get; set; }
        public string Hours { get; set; }
        public int SwipeCount { get; set; }
        public string CategoryName { get; set; } // Category name
    }
}
