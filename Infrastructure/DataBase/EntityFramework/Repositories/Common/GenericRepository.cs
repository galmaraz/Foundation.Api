using Domain.Repositories.Common;
using Infrastructure.DataBase.EntityFramework.Context;
using Infrastructure.DataBase.EntityFramework.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.EntityFramework.Repositories.Common;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, IIdentifiable
{
    protected readonly FoundationDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    protected GenericRepository(
        FoundationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity) 
    {
        var entityEntry = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public Task<TEntity?> GetByIdAsync(int? id)
    {
        return Task.FromResult<TEntity?>(_dbSet.Find(id));
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
    
    public async Task<bool> DeleteHardAsync(int id) 
    {
        var del = await _dbSet.FindAsync(id);
        if (del is null) return false; 
        _dbSet.Remove(del);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }
}