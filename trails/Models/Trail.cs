using System.ComponentModel.DataAnnotations;
namespace trails.Models {
    public abstract class BaseEntity {}
    public class Trail : BaseEntity {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MinLength(10)]
        public string Description { get; set; }
        public int Length { get; set; }
        public int Elevation { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }
}