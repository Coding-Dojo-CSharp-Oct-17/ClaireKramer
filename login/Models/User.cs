using System.ComponentModel.DataAnnotations;

namespace login.Models {
    public class User {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-z]+$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-z]+$")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [Compare("Confirm", ErrorMessage="Passwords do not match")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        public string Confirm { get; set; }
    }
}