using System.Linq.Expressions;
using Application;
using Application.Commons;
using Application.IServices.Base;
using Application.IServices.GoogleServices;
using Application.IServices.ModelServices;
using Domain.Enums;
using Domain.Models;
using Google.Apis.Auth;
using Infrastructure.Configurations.Google;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.ModelServices;

public class UserService : IUserService
{
    private readonly OAuthConfiguration _googleConfig;
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericService<User> _genericService;

    public UserService(IOptions<OAuthConfiguration> googleConfig, IUnitOfWork unitOfWork,IGenericService<User> genericService)
    {
        _googleConfig = googleConfig.Value;
        _unitOfWork = unitOfWork;
        _genericService = genericService;
    }
    public GoogleJsonWebSignature.Payload? VerifyGoogleToken(string idToken)
    {
        GoogleJsonWebSignature.ValidationSettings validationSettings = new GoogleJsonWebSignature.ValidationSettings();
        //validationSettings.Audience = new[] { _googleConfig.ClientId };
        return GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings).Result;
    }
    
    public async Task<User> ValidateLoginAsync(string? authHeader)
        {
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                string token = authHeader.Substring("Bearer ".Length).Trim();
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var payload = VerifyGoogleToken(token);
                        if(payload != null)
                        {
                            var user = await ValidateEmailAsync(email: payload.Email);
                            if(user == null)
                            {
                                user = await Register(payload);
                            }
                            return user;
                        } else
                        {
                            throw new InvalidJwtException("Unable to load payload!");
                        }                        
                    }
                    catch
                    {
                        throw new InvalidJwtException("Invalid token");
                    }
                }
            }
            throw new Exception("Unauthorized");
        }
        private async Task<User?> ValidateEmailAsync(string email)
        {
            var find = await _unitOfWork.GetRepository<User>().GetAll(include: "");
            return find.FirstOrDefault(u => u.Email == email);
        }
        public async Task<User> Register(GoogleJsonWebSignature.Payload payload)
        {
            var users = _unitOfWork.GetRepository<User>().GetAll(include: "");
            var firstOrDefault = users.Result.FirstOrDefault(u => u.Email == payload.Email);
            
            if (firstOrDefault != null)
            {
                return firstOrDefault;
            }
            
            var email = payload.Email;
            var user = new User
            {
                Email = payload.Email,
                
            };
            if (email.Substring(email.IndexOf('@') + 1) == "fpt.edu.vn")
            {
                user.Role = UserRole.Admin;
            }
            else
            {
                user.Role = UserRole.Customer;
            }
            await _genericService.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }
}