using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ISD_Project.Server.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICryptoService _cryptoService;
        public UserAccountService(ApplicationDbContext dbContext, ICryptoService cryptoService)
        {
            this._dbContext = dbContext;
            this._cryptoService = cryptoService;
        }
        public IActionResult Register(UserRegisterRequest request)
        {
            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if(request is null)
                    {
                        return new BadRequestObjectResult("Request is null");
                    }

                    if (_dbContext.UserAccounts.Any(u => u.Email == request.Email))
                    {
                        return new BadRequestObjectResult("User already exists");
                    }

                    _cryptoService.CreatePasswordHash(request.Password,
                        out byte[] passwordHash, out byte[] passwordSalt);


                    var user = new UserAccount
                    {
                        Email = request.Email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        VerificationToken = _cryptoService.CreateRandomToken()
                    };

                    _dbContext.UserAccounts.Add(user);
                    _dbContext.SaveChanges();

                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = (int)RoleType.Customer
                    };

                    _dbContext.UserRoles.Add(userRole);
                    _dbContext.SaveChanges();

                    transaction.Commit(); // Commit transaction if all commands succeed
                    return new OkObjectResult("User successfully created");
                }
                catch (Exception)
                {
                    transaction.Rollback(); // Rollback transaction if exception occurs
                    return new StatusCodeResult(500); // Internal Server Error
                }
            }
        }
        public IActionResult Login(UserLoginRequest request)
        {
            var user = _dbContext.UserAccounts.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return new BadRequestObjectResult("User does not exist");
            }

            if (!_cryptoService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new BadRequestObjectResult("Password is incorrect");
            }
            if (user.VerificationToken != null)
            {
                Verify(user.VerificationToken);
            }

            if (user.VerifiedAt == null)
            {
                return new BadRequestObjectResult("User does not verified");
            }

            return new OkObjectResult($"Login Successfully, Hello {user.Email}");
        }
        public IActionResult Verify(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new BadRequestObjectResult("Token is null or empty");
            }

            var user = _dbContext.UserAccounts.FirstOrDefault(u => u.VerificationToken == token);
            if (user == null)
            {
                return new BadRequestObjectResult("Invalid token");
            }

            user.VerifiedAt = System.DateTime.UtcNow;
            _dbContext.SaveChanges();
            return new OkObjectResult("User Verified");
        }
        public IActionResult GetUserRole(int userId)
        {
            var userRoles = _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name).ToList();
            if (userRoles == null || userRoles.Count == 0 )
            {
                return new NotFoundObjectResult("Not Found");
            }
            return new OkObjectResult(userRoles);
        }
        public IActionResult ForgotPassword(UserForgotPasswordRequest request)
        {
            var user = _dbContext.UserAccounts.FirstOrDefault(u => u.Email == request.Email);
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
            var user = _dbContext.UserAccounts.FirstOrDefault(u => u.PasswordResetToken == request.Token);
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
