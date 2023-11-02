﻿namespace MyProject.Core.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
