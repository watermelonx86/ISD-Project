using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            using(var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if(request is null)
                    {
                        return new BadRequestObjectResult("Request is null");
                    }

                    if (await _dbContext.UserAccounts.AnyAsync(u => u.Email == request.Email))
                    {
                        return new BadRequestObjectResult("User already exists");
                    }

                    var (passwordHash, passwordSalt) = await _cryptoService.CreatePasswordHash(request.Password);


                    var user = new UserAccount
                    {
                        Email = request.Email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        //VerificationToken = await _cryptoService.CreateRandomToken()
                    };

                    var token = await _cryptoService.CreateToken(user);
                    if (token != null)
                    {
                        user.VerificationToken = token;
                    }
                    else
                    {
                        return new BadRequestObjectResult("Token is null");
                    }
                    await _dbContext.UserAccounts.AddAsync(user);
                    await _dbContext.SaveChangesAsync();

                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = (int)RoleType.Customer
                    };

                    await _dbContext.UserRoles.AddAsync(userRole);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync(); // Commit transaction if all commands succeed
                    return new OkObjectResult("User successfully created");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(); // Rollback transaction if exception occurs
                    return new StatusCodeResult(500); // Internal Server Error
                }
            }
        }
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return new BadRequestObjectResult("User does not exist");
            }

            if (!await _cryptoService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new BadRequestObjectResult("Password is incorrect");
            }

            if (user.VerificationToken != null)
            {
                await Verify(user.VerificationToken);
            }

            if (user.VerifiedAt == null)
            {
                return new BadRequestObjectResult("User does not verified");
            }

            var userDto = new UserLoginResponse
            {
                UserAccountId = user.Id,
                Token = user.VerificationToken,
                Role = await GetUserRole(user.Id),
                UserId = user.UserId ?? 0,
                IsActivated = user.IsActivated
            };


            return new OkObjectResult(userDto);
        }
        public async Task<IActionResult> Verify(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new BadRequestObjectResult("Token is null or empty");
            }

            var user = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null)
            {
                return new BadRequestObjectResult("Invalid token");
            }

            user.VerifiedAt = System.DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult("User Verified");
        }
        public async Task<List<String>> GetUserRole(int userId)
        {
            var userRoles = await _dbContext.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name).ToListAsync();
            if (userRoles == null || userRoles.Count == 0 )
            {
                return new List<string>();
            }
            return userRoles;
        }
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordRequest request)
        {
            var user = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return new BadRequestObjectResult("User does not exist");
            }
            user.PasswordResetToken = await _cryptoService.CreateRandomToken();
            user.RestTokenExpires = System.DateTime.UtcNow.AddHours(1);
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult("You may now reset your password");
        }
        public async Task<IActionResult> ResetPassword(UserResetPasswordRequest request)
        {
            var user = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null)
            {
                return new BadRequestObjectResult("Invalid token");
            }
            if (user.RestTokenExpires < System.DateTime.UtcNow)
            {
                return new BadRequestObjectResult("Token has expired");
            }
            var (passwordHash, passwordSalt) = await _cryptoService.CreatePasswordHash(request.Password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.RestTokenExpires = null;

            await _dbContext.SaveChangesAsync();
            return new OkObjectResult("Password successfully reset");
        }
    }
}
