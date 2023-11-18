using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public UserController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists - Tài khoản đã tồn tại trong hệ thống");
            }

            CreatePasswordHash(request.Password,
                out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return Ok("User successfully created");
        }

        //TODO: violates principles single responsibility
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        //TODO: violates principles single responsibility
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
