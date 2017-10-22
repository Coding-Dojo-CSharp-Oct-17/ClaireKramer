using System;
using System.ComponentModel.DataAnnotations;

namespace restauranter.Models {
    public class Review {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Reviewer Name:")]
        public string Reviewer { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Restaurant Name:")]
        public string Restaurant { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Review:")]
        [MinLength(10)]
        public string Content { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Date of Visit:")]
        public DateTime VisitDate { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Stars:")]
        public int Stars { get; set; }

    }
}