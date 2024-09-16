using Rentley.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentley
{
    public class varify_image
    {
        [Key]
        public int img_id { get; set; }
        [Required]
        public IFormFile Front { get; set; }
        [Required]
        public IFormFile Back { get; set; }
        [Required]
        public IFormFile Self { get; set; }
    }
}
