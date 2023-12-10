using ISD_Project.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface ICryptoService
    {

        Task<(byte[] passwordHash, byte[] passwordSalt)> CreatePasswordHashAsync(string password);
        Task<string> CreateRandomTokenAsync();
        Task<string> CreateTokenAsync(UserAccount userAccount);
        Task<bool> VerifyPasswordHashAsync(string password, byte[] storedHash, byte[] storedSalt);

    }
}
