using System;
using System.ComponentModel.DataAnnotations;

namespace wall.Models {
    public class User {
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage="First Name must be at least 2 Characters")]
        [RegularExpression(@"^[a-zA-z]+$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2, ErrorMessage="Last Name must be at least 2 Characters")]
        [RegularExpression(@"^[a-zA-z]+$")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage="Password must be at least 8 Characters")]
        [Compare("Confirm", ErrorMessage="Passwords do not match")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        public string Confirm { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}