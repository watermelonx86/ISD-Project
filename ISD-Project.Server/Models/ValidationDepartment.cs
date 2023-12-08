namespace ISD_Project.Server.Models
{
    public class ValidationDepartment : User
    {
        public ValidationDepartment() : base()
        {

        }

        public ValidationDepartment(string email, int userAccountId, UserAccount userAccount) : base(email, userAccountId, userAccount)
        {

        }
    }
}
