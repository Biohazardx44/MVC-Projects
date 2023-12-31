﻿namespace BurgerApp.Domain.Models
{
    public class Location : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime OpensAt { get; set; }
        public DateTime ClosesAt { get; set; }
    }
}