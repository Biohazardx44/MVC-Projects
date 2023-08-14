﻿using PizzaApp.Domain.Enums;

namespace PizzaApp.Domain.Models
{
    public class Pizza : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsOnPromotion { get; set; }
        public PizzaSize PizzaSize { get; set; }
        public List<PizzaOrder> PizzaOrders { get; set; }
    }
}