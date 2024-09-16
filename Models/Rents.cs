using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rentley.Models
{
    public class Rents
    {
        [Key]
        public int RentRequestId { get; set; }

        [Required]
        public int User_id { get; set; }
        [ForeignKey("User_id")]
        public virtual User_temp User_Temp { get; set; }

        [Required]
        public int Prod_id { get; set; }
        [ForeignKey("Prod_id")]
        public virtual products Products { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User_temp Owner { get; set; }

        [Required]
        public string status { get; set; } = "Pending";

        [Required]
        public string Payment_Status { get; set; } = "------------";

        [Required]
        public string Rent_Days { get; set; } = "-----";

        [Required]
        public int Sub_Total { get; set; } = 0;

        [Required]
        public decimal Fee { get; set; } = 0m;

        [Required]
        public decimal Total { get; set; } = 0m;

        [Required]
        public string AccountName { get; set; } = "-------";

        [Required]
        public string AccountNo { get; set; } = "-------";

        [Required]
        public string PaymentMethod { get; set; } = "-------";

        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan FromTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UntilDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan UntilTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
