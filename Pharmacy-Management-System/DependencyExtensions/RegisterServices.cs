using FluentValidation;
using Pharmacy_Management_System.Application.Dtos.Requests.Users;
using Pharmacy_Management_System.Application.ServiceInterfaces.Email;
using Pharmacy_Management_System.Application.ServiceInterfaces.Permissions;
using Pharmacy_Management_System.Application.ServiceInterfaces.Roles;
using Pharmacy_Management_System.Application.ServiceInterfaces.Tests;
using Pharmacy_Management_System.Application.Services.Users;
using Pharmacy_Management_System.Service.Services.Email;
using Pharmacy_Management_System.Service.Services.Permissions;
using Pharmacy_Management_System.Service.Services.Roles;
using Pharmacy_Management_System.Service.Services.Tests;
using Pharmacy_Management_System.Service.Services.Users;
using Pharmacy_Management_System.Service.Validators.Users;

namespace Pharmacy_Management_System.DependencyExtensions
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
        }
    }
}
