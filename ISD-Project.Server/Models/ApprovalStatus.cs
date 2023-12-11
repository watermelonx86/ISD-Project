using System.ComponentModel.DataAnnotations;

namespace ISD_Project.Server.Models
{
    public class ApprovalStatus
    {
        [Key]
        public int Id { get; set; }
        // Foreign key to Customer
        public int CustomerId { get; set; }
        public required Customer Customer { get; set; }
        //Foreign key to Insurance
        public int InsuranceId { get; set; }
        public required Insurance Insurance { get; set; }

        // Foreign key to ValidateDepartment
        public int ValidationDepartmentId { get; set; }
        public required ValidationDepartment ValidationDepartment { get; set; }
        public ProfileStatus ProfileStatus { get; set; }
        public DateOnly ApprovalDate { get; set; }
        public string ApprovalComment { get; set; } = String.Empty;
    }
}