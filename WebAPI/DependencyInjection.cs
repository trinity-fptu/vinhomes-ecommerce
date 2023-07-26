using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application;
using Application.IRepositories;
using Application.IRepositories.Base;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Controllers;
using WebAPI.CustomMiddleware;

namespace WebAPI;

public static class DependencyInjection
{
    public static void AddSerivces(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod();
                              });
        });
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://securetoken.google.com/YOUR_PROJECT_ID";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/YOUR_PROJECT_ID",
                    ValidateAudience = true,
                    ValidAudience = "YOUR_PROJECT_ID",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var token = context.SecurityToken as JwtSecurityToken;
                        if (token == null)
                        {
                            context.Fail("Failed to validate ID token");
                            return;
                        }

                        var uid = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        if (uid == null)
                        {
                            context.Fail("UID claim missing in ID token");
                            return;
                        }

                        // TODO: add additional checks if needed
                        // e.g., check the uid against your database to ensure the user is allowed to access your system
                    }
                };
            });
        var firebaseProjectId = builder.Configuration["FirebaseProjectId"];
        services.AddSingleton<FirebaseAuthentication>(_ => new FirebaseAuthentication(firebaseProjectId));
    }
}
