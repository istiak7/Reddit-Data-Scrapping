using Reddit_Management_System.Application.Repositories.Users;
using Reddit_Management_System.Application.RepositoryInterfaces.Permissions;
using Reddit_Management_System.Application.RepositoryInterfaces.Roles;
using Reddit_Management_System.Application.RepositoryInterfaces.RolesPermissions;
using Reddit_Management_System.Application.ServiceInterfaces.Roles;
using Reddit_Management_System.Repo.Repositories.Permissions;
using Reddit_Management_System.Repo.Repositories.Roles;
using Reddit_Management_System.Repo.Repositories.RolesPermissions;
using Reddit_Management_System.Repo.Repositories.Users;
using Reddit_Management_System.Service.Services.Roles;

namespace Reddit_Management_System.DependencyExtensions
{
    public static class RegisterRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleCommandRepository, RoleCommandRepository>();
            services.AddScoped<IPermissionCommandRepository, PermissionCommandRepository>();
            services.AddScoped<IRolePermissionCommandRepository, RolePermissionCommandRepository>();
        }
    }
}
