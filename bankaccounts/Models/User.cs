using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace bankaccounts.Models {
    public class User {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Display(Name = "Email:")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password:")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPW { get; set; }
        public int Balance { get; set; }
        public User() {
            Transcations = new List<Transcation>();
        }
        public List<Transcation> Transcations { get; set; }
        
    }
}