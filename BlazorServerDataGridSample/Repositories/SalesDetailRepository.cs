using BlazorServerDataGridSample.Data;
using BlazorServerDataGridSample.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace BlazorServerDataGridSample.Repositories;

public class SalesDetailRepository : DetailRepository<SampleDbContext, SalesDetail>, ISalesDetailRepository
{
    public SalesDetailRepository(IDbContextFactory<SampleDbContext> context, ILogger<SalesDetailRepository> logger)
        : base(context, logger)
    {
    }

    public async ValueTask UpdateAllAsync(IEnumerable<SalesDetail> entities, ISet<int> sets)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        //登録、更新処理
        foreach (var item in entities)
        {
            if (item.Id != 0)
            {
                var entry = await context.Set<SalesDetail>().FindAsync(item.Id);
                if (entry == null) throw new InvalidOperationException($"指定されたエンティティを更新しようとしましたが見つかりません。id = {item.Id}");
                _mapper.Map(item, entry);
            }
            else
            {
                await context.Set<SalesDetail>().AddAsync(item);
            }
        }

        //削除処理
        foreach (var item in sets)
        {
            if (item != 0)
            {
                var entry = await context.Set<SalesDetail>().FindAsync(item);
                if (entry == null) throw new InvalidOperationException($"指定されたエンティティを更新しようとしましたが見つかりません。id = {item}");

                context.SalesDetails.Remove(entry);
            }
        }

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, ex.Message); // ログ出力等をここで実装
            throw;
        }
    }
}