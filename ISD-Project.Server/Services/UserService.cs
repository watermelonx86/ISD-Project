using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICryptoService _cryptoService;
        public UserService(ApplicationDbContext dbContext, ICryptoService cryptoService)
        {
            this._dbContext = dbContext;
            this._cryptoService = cryptoService;
        }
        public IActionResult Register(UserRegisterRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
            {
                return new BadRequestObjectResult("User already exists");
            }

            _cryptoService.CreatePasswordHash(request.Password,
                out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = _cryptoService.CreateRandomToken()
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return new OkObjectResult("User successfully created");
        }
        public IActionResult Login(UserLoginRequest request) {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return new BadRequestObjectResult("User does not exist");
            }

            if (!_cryptoService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new BadRequestObjectResult("Password is incorrect");
            }
            Verify(user.VerificationToken);

            if (user.VerifiedAt == null)
            {
                return new BadRequestObjectResult("User does not verified");
            }

            return new OkObjectResult($"Login Successfully, Hello {user.Email}");
        }

        public IActionResult Verify(string token)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.VerificationToken == token);
            if (user == null)
            {
                return new BadRequestObjectResult("Invalid token");
            }

            //Luu y: Phai dung .UtcNow hay vi .Now, de tranh bi loi PostgreSQL
            //ref more about UtcNow: https://stackoverflow.com/questions/62151/datetime-now-vs-datetime-utcnow
            user.VerifiedAt = System.DateTime.UtcNow;
            _dbContext.SaveChanges();
            return new OkObjectResult("User Verified");
        }

        public IActionResult ForgotPassword(UserForgotPasswordRequest request) 
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return new BadRequestObjectResult("User does not exist");
            }
            user.PasswordResetToken = _cryptoService.CreateRandomToken();
            user.RestTokenExpires = System.DateTime.UtcNow.AddHours(1);
            _dbContext.SaveChanges();
            return new OkObjectResult("You may now reset your password");
        }
        public IActionResult ResetPassword(UserResetPasswordRequest request) 
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.PasswordResetToken == request.Token);
            if (user == null)
            {
                return new BadRequestObjectResult("Invalid token");
            }
            if (user.RestTokenExpires < System.DateTime.UtcNow)
            {
                return new BadRequestObjectResult("Token has expired");
            }
            _cryptoService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.RestTokenExpires = null;

            _dbContext.SaveChanges();
            return new OkObjectResult("Password successfully reset");
        }
    }
}
