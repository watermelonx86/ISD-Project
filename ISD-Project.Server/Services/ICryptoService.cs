using ISD_Project.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface ICryptoService
    {
        //TODO: Using asynchronous methods

        Task<(byte[] passwordHash, byte[] passwordSalt)> CreatePasswordHash(string password);
        Task<string> CreateRandomToken();
        Task<string> CreateToken(UserAccount userAccount);
        Task<bool> VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

    }
}
