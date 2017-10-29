using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace commerce.Models {
    public class Product {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int ProductQuantity { get; set; }
        public List<Order> Orders { get; set; }
        public Product() {
            Orders = new List<Order>();
        }
    }
}