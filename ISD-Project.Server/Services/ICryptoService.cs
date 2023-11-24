using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface ICryptoService
    {
        //TODO: Using asynchronous methods

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        string CreateRandomToken();
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

    }
}
