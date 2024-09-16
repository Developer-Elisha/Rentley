using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentley.Models
{
    public class Verify_Img
    {
        [Key]
        public int Img_id { get; set; }

        [Required]
        public string FrontCNIC { get; set; }

        [Required]
        public string BackCNIC { get; set; }

        [Required]
        public string Selfie { get; set; }
    }
}
