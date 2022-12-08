namespace BlazorServerDataGridSample.Services;

/// <summary>
/// マスター制御用サービスインターフェース
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IMasterService<TEntity>
{
    ValueTask<TEntity?> GetAsync(int id);

    ValueTask<IEnumerable<TEntity>> GetAllAsync();

    ValueTask AddAsync(TEntity entity);

    ValueTask UpdateAsync(TEntity entity, int id);

    ValueTask RemoveAsync(int id);
}