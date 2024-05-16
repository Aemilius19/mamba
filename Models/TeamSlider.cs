using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class TeamSlider
    {
        public int ID { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Fullname{ get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Position { get; set; }
        [MinLength(1)]
        [MaxLength(255)]
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
