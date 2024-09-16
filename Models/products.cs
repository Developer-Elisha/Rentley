using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rentley.Models
{
    public class products
    {
        [Key] 
        public int Prod_id { get; set; }

        [Required]
        public int User_id { get; set; }
        [ForeignKey("User_id")]
        public virtual User_temp User_Temp { get; set; }

        [Required]
        public string Prod_name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Category_Id { get; set; }
        [ForeignKey("Category_Id")]
        public virtual category Category { get; set; }

        [Required]
        public int PricePerDay { get; set; }

        [Required]
        public string Product_Image { get; set; }

        [Required]
        public string Availability { get; set; }
    }
}
