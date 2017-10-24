using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace weddingplanner.Models {
    public class User {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage="Passwords do not match")]
        [Display(Name = "Confirm Password:")]
        public string ConfirmPW { get; set; }
        public List<Rsvp> Rsvps { get; set; }
        public User() {
            Rsvps = new List<Rsvp>();
        }
    }
}