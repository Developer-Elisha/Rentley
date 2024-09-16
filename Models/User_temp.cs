using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rentley.Models
{
    public class User_temp
    {
        [Key]
        public int User_id { get; set; }    

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public int Postal_Code { get; set; }
        [Required]
        public int Age { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Role { get; set; }
        [Required]
        public string status { get; set; } = "Not-Approved";
    }
}
