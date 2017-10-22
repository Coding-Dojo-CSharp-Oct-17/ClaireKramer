using System;
using System.ComponentModel.DataAnnotations;
namespace dojoleague.Models {
    public abstract class BaseEntity {}
    public class Ninja : BaseEntity {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Ninja Name:")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Ninjaing Level (1-10):")]
        [Range(1,10, ErrorMessage = "Level must be between 1 and 10")]
        public int Level { get; set; }
        [Display(Name = "Optional Description:")]
        public string Description { get; set; }
        [Display(Name = "Dojo Location:")]
        public Dojo dojo { get; set; }

    }
}