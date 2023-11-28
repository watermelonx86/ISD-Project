using System.ComponentModel.DataAnnotations;

namespace ISD_Project.Server.Models
{
    public class HealthInformation
    {
        [Key]
        public int Id { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; } 
        public double BMI { get; set; }
        public List<string>? MedicalHistory { get; set; } // Lịch sử bệnh lý
        public List<string>? CurrentMedications { get; set; } // Thuốc đang sử dụng
        public List<string>? VaccinationHistory { get; set; } // Lịch sử tiêm phòng
        public List<string>? LifestyleHabits { get; set; } // Thói quen và lối sống
        public DateTime LastUpdate { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
