using Microsoft.EntityFrameworkCore;
using Reddit_Management_System.Application.Common.Utilities;
using Reddit_Management_System.Data.DbContexts.ModelBuilders;
using Reddit_Management_System.Domain.Contexts;
using Reddit_Management_System.Domain.Entities;
using Reddit_Management_System.Domain.Entities.Permissions;
using Reddit_Management_System.Domain.Entities.Roles;
using Reddit_Management_System.Domain.Entities.Users;
using System.Reflection;
using Reddit_Management_System.Domain.Entities.Subscribers;

namespace Reddit_Management_System.Data.DbContexts
{
    public partial class ApplicationDbContextWrite : DbContext, IApplicationDbContext
    {
        #region Constructor
        public ApplicationDbContextWrite()
        {

        }

        public ApplicationDbContextWrite(DbContextOptions<ApplicationDbContextWrite> options) : base(options)
        {

        }
        #endregion Constructor

        #region DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ConfigureAllModelBuilders();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        #region SaveChanges

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            int result = 0;
            foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetDefaultValueDuringInsert(CommonMethods.GetBDCurrentTime());
                        break;

                    case EntityState.Modified:
                        entry.Entity.SetDefaultValueDuringUpdate(CommonMethods.GetBDCurrentTime());
                        break;
                }
            }
            result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        #endregion
    }
}
