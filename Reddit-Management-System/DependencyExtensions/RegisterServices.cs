using FluentValidation;
using Reddit_Management_System.Application.Dtos.Requests.Users;
using Reddit_Management_System.Application.ServiceInterfaces.AI;
using Reddit_Management_System.Application.ServiceInterfaces.Email;
using Reddit_Management_System.Application.ServiceInterfaces.Permissions;
using Reddit_Management_System.Application.ServiceInterfaces.Roles;
using Reddit_Management_System.Application.ServiceInterfaces.Tests;
using Reddit_Management_System.Application.Services.Users;
using Reddit_Management_System.Service.Services.Email;
using Reddit_Management_System.Service.Services.Permissions;
using Reddit_Management_System.Service.Services.Roles;
using Reddit_Management_System.Service.Services.Tests;
using Reddit_Management_System.Service.Services.Users;
using Reddit_Management_System.Service.Validators.Users;
using Reddit_Management_System.Application.ServiceInterfaces.Reddit;
using Reddit_Management_System.Service.Services.AI;
using Reddit_Management_System.Service.Services.Reddit;

namespace Reddit_Management_System.DependencyExtensions
{
    public static class RegisterServices
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleCommandService, RoleCommandService>();
            services.AddScoped<IPermissionCommandService, PermissionCommandService>();
            services.AddScoped<ITestApiQueryService, TestApiQueryService>();
            services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginRequest>();
            services.AddScoped<IEmailCommandService, EmailCommandService>();
            services.AddScoped<IRedditQueryService, RedditQueryService>();
            services.AddScoped<IGeminiComamndService, GeminiCommandService>();
        }
    }
}
