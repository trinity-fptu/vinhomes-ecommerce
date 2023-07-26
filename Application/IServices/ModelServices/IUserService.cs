using System.Linq.Expressions;
using Application.Commons;
using Application.IServices.Base;
using Domain.Models;
using Google.Apis.Auth;

namespace Application.IServices.ModelServices;

public interface IUserService { 
    Task<User> ValidateLoginAsync(string? authHeader);
}