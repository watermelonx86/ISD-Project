using ISD_Project.Server.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ISD_Project.Server.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly ApplicationDbContext _dbContext;
        public CryptoService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<(byte[] passwordHash, byte[] passwordSalt)> CreatePasswordHash(string password)
        {
            return await Task.Run(() =>
            {
                using (var hmac = new HMACSHA256())
                {
                    return (
                        passwordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                        passwordSalt: hmac.Key
                    );
                }
            });
        }
        public async Task<string> CreateRandomToken()
        {
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            if (await _dbContext.UserAccounts.AnyAsync(u => u.VerificationToken == token))
            {
                await CreateRandomToken();
            }
            return token;
        }
        public async Task<bool> VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            return await Task.Run(() =>
            {
                using (var hmac = new HMACSHA256(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    //Phai su dung SequenceEqual vi day la byte[] array
                    return computedHash.SequenceEqual(passwordHash);
                }
            });
        }
    }
}
