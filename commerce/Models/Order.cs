using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace commerce.Models {
    public class Order {
        public int OrderId { get; set; }
        public int OrderQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}