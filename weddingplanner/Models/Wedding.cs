using System;
using System.ComponentModel.DataAnnotations;

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

    }
}