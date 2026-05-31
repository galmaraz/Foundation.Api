using Infrastructure.DataBase.EntityFramework.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.EntityFramework.Context;

public class FoundationDbContext: DbContext
{
    //Example
    //public DbSet<BrandsEntity> Brands { get; set; }
    
    public FoundationDbContext(DbContextOptions<FoundationDbContext> options) : base(options)
    {
        // Configurado como NoTracking por defecto para optimizar consultas de solo lectura
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Example
        //builder.ApplyConfiguration(new BrandsConfiguration());
        
        base.OnModelCreating(builder); 
    }
    
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void UpdateAuditFields()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = GetCurrentUserId();
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = GetCurrentUserId();
                    break;

                case EntityState.Modified:
                    entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                    entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = 104;
                    break;
            }
        }
    }
    
    private int GetCurrentUserId()
    {
        return 123;
    }
}