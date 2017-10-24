using System;
using System.ComponentModel.DataAnnotations;

namespace weddingplanner.Models {
    public class User {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage="Passwords do not match")]
        public string ConfirmPW { get; set; }
    }
}