using System.Reflection;
using ProductCRUD.Application.Common.Interfaces;
using ProductCRUD.Domain.Entities;
using ProductCRUD.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductCRUD.Domain.Common;

namespace ProductCRUD.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IUser _user;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUser user) : base(options) 
    {
        _user = user;
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
        /// Hooks for Commands
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<BaseAuditableEntity> entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _user.Id.ToString();
                        entry.Entity.Created = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy =  _user.Id.ToString();
                        entry.Entity.LastModified = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Deleted;
                        entry.Entity.DeletedAt = DateTimeOffset.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
}
