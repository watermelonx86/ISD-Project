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
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public string CreateRandomToken()
        {
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            if (_dbContext.Users.Any(u => u.VerificationToken == token))
            {
                CreateRandomToken();
            }
            return token;
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //Phai su dung SequenceEqual vi day la byte[] array
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
