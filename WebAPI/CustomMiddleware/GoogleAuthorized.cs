using System.Net;
using Application.IServices.ModelServices;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.CustomMiddleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class GoogleAuthorized : ActionFilterAttribute, IActionFilter
{
    private IUserService _userServices;
    private UserRole[] _requiredRoles;
    public GoogleAuthorized(params UserRole[] roles)
    {
        _requiredRoles = roles;
    }
    
    public void SetConfiguration(ActionExecutingContext context)
    {
        var serviceProvider = context.HttpContext.RequestServices;
        _userServices = (IUserService)serviceProvider.GetRequiredService(typeof(IUserService));
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        bool isAuthorized = true;
        string authHeader = context.HttpContext.Request.Headers["Authorization"];
        Console.WriteLine(authHeader);
        SetConfiguration(context);
        User? authorized;
        string message = "Unauthorized";
        try
        {
            authorized = _userServices.ValidateLoginAsync(authHeader).Result;
            bool roleAuthorized = true;
            if (_requiredRoles.Length > 0 && !_requiredRoles.Any(r => r == authorized.Role))
            {
                //role authorizing
                roleAuthorized = false;
                message += " - Unauthorized for this function!";
            }
            isAuthorized = roleAuthorized;
            if (!isAuthorized)
            {
                context.Result = new JsonResult(message + GetAlertRequiredRoles())
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }
        }
        catch (Exception)
        {
            isAuthorized = false;
        }
        if (!isAuthorized)
        {
            context.Result = new JsonResult(message)
            {
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
        }
    }

    private string GetAlertRequiredRoles()
    {
        return $"Required roles: {string.Join(", ", _requiredRoles)}";
    }
}