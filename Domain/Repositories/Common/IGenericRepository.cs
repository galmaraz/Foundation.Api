namespace Domain.Repositories.Common;

public interface IGenericRepository<TModel> where TModel: class
{
    public Task<TModel> CreateAsync(TModel model);
    public Task<TModel?> GetByIdAsync(int? id);
    public Task<bool>DeleteHardAsync(int id);
    public Task<bool>ExistsByIdAsync(int id);
}