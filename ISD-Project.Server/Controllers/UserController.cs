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

        //TODO: Avoid providing too much information in error messages
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
        //TODO: Avoid providing too much information in error messages
        [HttpPost("login")]
        public IActionResult Login(UserLoginRequest request)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if(user == null)
            {
                   return BadRequest("User does not exist - Tài khoản không tồn tại trong hệ thống");
            }
            
            if(!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect");
            }

            if (user.VerifiedAt == null)
            {
                return BadRequest("User does not verified");
            }

            return Ok($"Login Successfully, Hello {user.Email}");
        }

        [HttpPost("verify")]
        public IActionResult Verify(string token)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.VerificationToken == token);
            if(user == null)
            {
                return BadRequest("Invalid token");
            }

            //Luu y: Phair dung .UtcNow hay vi .Now, de tranh bi loi PostgreSQL
            user.VerifiedAt = System.DateTime.UtcNow;
            _dbContext.SaveChanges();
            return Ok("User Verified");
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

        //TODO: violates principles single responsibility
        private bool VerifyPasswordHash(string password, byte[] passwordHash ,byte[] passwordSalt) {
            using(var hmac = new HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //Phai su dung SequenceEqual vi day la byte[] array
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
