﻿namespace ToDuoAPI.Models
{
    public class ToDuoCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Adventures> Adventures { get; set; }
    }
}
