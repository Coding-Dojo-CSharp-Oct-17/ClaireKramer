using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace commerce.Models {
    public class Customer {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Order> Orders { get; set; }
        public Customer() {
            Orders = new List<Order>();
        }
    }
}