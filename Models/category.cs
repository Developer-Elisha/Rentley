using System.ComponentModel.DataAnnotations;

namespace Rentley.Models
{
    public class category
    {
        [Key]
        public int Category_Id { get; set; }

        [Required]

        public string Category_Name { get; set; }
    }
}
