
namespace ISD_Project.Server.Services
{
    public class HealthInformationService : IHealthInformationService
    {
        //TODO: Add more options
        public Lazy<List<string>> GetCurrentMedications()
        {
            return new Lazy<List<string>>(() => new List<string>
            {
                "Cảm cúm",
                "Đau dạ dày",
                "Tiểu đường",
                "Huyết áp cao",
                "Đau lưng",
                "Viêm khớp",
                "Tiêu chảy",
                "Migraine",
                "Thận thấp",
                "Tiền sử ung thư",
                "Đau tim",
                "Viêm gan",
                "Thấp khớp",
                "Đau cơ",
                "Gout",
                "Bệnh tiêu hóa",
                "Bệnh tăng lipid máu"
            });
        }

        public Lazy<List<string>> GetMedicalHistory()
        {
            return new Lazy<List<string>>(() => new List<string>
            {
                "Bệnh tinh mạch",
                "Bệnh thận đa nang",
                "Bệnh thận đa polyp",
                "Parkinson",
                "Xơ cứng bì",
                "Viêm gan B",
                "Uốn ván",
                "Rubella",
                "Viêm gan A",
                "HPV",
                "HIB",
                "Bạch hầu"
            
             });
        }

        public Lazy<List<string>> GetVaccinationHistory()
        {
            return new Lazy<List<string>>(() => new List<string>()
            {

            });
        }

        public Lazy<List<string>> LifestyleHabits()
        {
            return new Lazy<List<string>>(() => new List<string>
            {
                "Hút thuốc lá",
                "Uống rượu",
                "Nghiện ma tuý đá",
                "Tập thể dục thường xuyên",
                "Ăn kiêng",
                "Sử dụng nhiều caffeine",
                "Tiếp xúc với môi trường độc hại",
                "Ăn uống ít chất béo",
                "Ăn uống nhiều đường",
                "Sử dụng mỹ phẩm hóa học",
                "Sử dụng nhiều đồ chứa chất BPA",
            });
    }
    }
}
