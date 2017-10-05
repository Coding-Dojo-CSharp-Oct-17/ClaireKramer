using System.ComponentModel.DataAnnotations;

namespace forms.Models {
    public class Home {
        [Required]
        [MinLength(4)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(4)]
        public string LastName { get; set; }
        [Required]
        [Range(1,99)]
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}