using System;
using System.ComponentModel.DataAnnotations;

namespace wall.Models {
    public class Message {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [MinLength(2)]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

    public class Comment {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MessageId { get; set; }
        [Required]
        [MinLength(2)]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}