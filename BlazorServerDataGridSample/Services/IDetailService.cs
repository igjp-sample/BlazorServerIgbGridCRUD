namespace BlazorServerDataGridSample.Services;

/// <summary>
/// 明細入力用サービスインターフェース
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IDetailService<TEntity>
{
    ValueTask<TEntity?> GetAsync(int id);

    ValueTask<IEnumerable<TEntity>> GetAllAsync();

    ValueTask AddAsync(TEntity entity);

    ValueTask UpdateAsync(TEntity entity, int id);

    ValueTask UpdateAllAsync(IList<TEntity> entities, ISet<int> sets);

    ValueTask RemoveAsync(int id);
}