using System; 
using System.ComponentModel.DataAnnotations;

namespace bankaccounts.Models {
    public class Transcation {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int User_Id { get; set; }
        public User User { get; set; }
    }
}