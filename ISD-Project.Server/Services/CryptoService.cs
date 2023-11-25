using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace ISD_Project.Server.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly ApplicationDbContext _dbContext;
        protected readonly IConfiguration _configuration;
        public CryptoService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            _configuration = configuration;
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

        public async Task<string> CreateToken(UserAccount userAccount)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userAccount.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult(jwt);
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
