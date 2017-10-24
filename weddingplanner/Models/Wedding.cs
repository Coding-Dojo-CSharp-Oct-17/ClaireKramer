using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace weddingplanner.Models {
    public class Wedding {
        public int WeddingId { get; set; }
        [Required]
        public string Groom { get; set; }
        [Required]
        public string Bride { get; set; }
        [Required]
        public DateTime WeddingDate { get; set; }
        [Required]
        public string Address { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<User> Guests { get; set; }
        public Wedding() {
            Guests = new List<User>();
        }

    }
}