using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //TODO: Avoid providing too much information in error messages Register, Login, Verify
    //TODO: Fix violates principles single responsibility in CreatePasswordHash, CreateRandomToken, VerifyPasswordHash


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
                return BadRequest("User already exists");
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
        [HttpPost("login")]
        public IActionResult Login(UserLoginRequest request)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect");
            }

            Verify(user.VerificationToken);

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
            if (user == null)
            {
                return BadRequest("Invalid token");
            }

            //Luu y: Phai dung .UtcNow hay vi .Now, de tranh bi loi PostgreSQL
            //ref more about UtcNow: https://stackoverflow.com/questions/62151/datetime-now-vs-datetime-utcnow
            user.VerifiedAt = System.DateTime.UtcNow;
            _dbContext.SaveChanges();
            return Ok("User Verified");
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(UserForgotPasswordRequest request)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            user.PasswordResetToken = CreateRandomToken();
            user.RestTokenExpires = System.DateTime.UtcNow.AddHours(1);
            _dbContext.SaveChanges();
            return Ok("You may now reset your password");
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(UserResetPasswordRequest request)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.PasswordResetToken == request.Token);
            if (user == null)
            {
                return BadRequest("Invalid token");
            }
            if (user.RestTokenExpires < System.DateTime.UtcNow)
            {
                return BadRequest("Token has expired");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.RestTokenExpires = null;

            _dbContext.SaveChanges();
            return Ok("Password successfully reset");
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private string CreateRandomToken()
        {
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            if (_dbContext.Users.Any(u => u.VerificationToken == token))
            {
                CreateRandomToken();
            }
            return token;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
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
