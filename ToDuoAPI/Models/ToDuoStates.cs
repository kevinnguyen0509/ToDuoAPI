namespace ToDuoAPI.Models
{
    public class ToDuoStates
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Collection of Adventures
        public virtual ICollection<Adventures> Adventures { get; set; }
    }
}
