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
        public decimal Length { get; set; }
        public int Elevation { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}