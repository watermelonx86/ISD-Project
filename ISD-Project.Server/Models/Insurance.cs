using System.ComponentModel.DataAnnotations;

namespace ISD_Project.Server.Models
{
    public class Insurance
    {
        [Key]
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public float PriceAmount { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

    }
}
