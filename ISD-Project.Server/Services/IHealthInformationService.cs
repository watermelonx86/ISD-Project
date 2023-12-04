namespace ISD_Project.Server.Services
{
    public interface IHealthInformationService
    {
        Lazy<List<string>> GetMedicalHistory();
        Lazy<List<string>> GetCurrentMedications();
        Lazy<List<string>> GetVaccinationHistory();
        Lazy<List<string>> LifestyleHabits();
    }
}
