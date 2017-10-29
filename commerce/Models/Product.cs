using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace commerce.Models {
    public class Product {
        public int ProductId { get; set; }
        [Display(Name = "Product Name:")]
        public string ProductName { get; set; }
        [Display(Name = "Image Url:")]
        public string ImageUrl { get; set; }
        [Display(Name = "Description:")]
        public string Description { get; set; }
        [Display(Name = "Initial Quantity")]
        public int ProductQuantity { get; set; }
        public List<Order> Orders { get; set; }
        public Product() {
            Orders = new List<Order>();
        }
    }
}