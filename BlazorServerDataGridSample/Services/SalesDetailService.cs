using AutoMapper;
using BlazorServerDataGridSample.Data.Models;
using BlazorServerDataGridSample.Data.ViewModels;
using BlazorServerDataGridSample.Repositories;

namespace BlazorServerDataGridSample.Services;

public class SalesDetailService : ISalesDetailService
{
    private readonly ISalesDetailRepository _SalesDetailRepository;

    private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<SalesDetail, SalesDetailViewModel>();
        cfg.CreateMap<SalesDetailViewModel, SalesDetail>();
    }).CreateMapper();

    public SalesDetailService(ISalesDetailRepository SalesDetailRepository)
    {
        _SalesDetailRepository = SalesDetailRepository;
    }

    public ValueTask AddAsync(SalesDetailViewModel entity)
    {
        return _SalesDetailRepository.AddAsync(_mapper.Map<SalesDetail>(entity));
    }

    public async ValueTask<SalesDetailViewModel?> GetAsync(int id)
    {
        var salesDetail = await _SalesDetailRepository.GetAsync(id);
        return _mapper.Map<SalesDetailViewModel>(salesDetail);
    }

    /// <summary>
    /// ユーザーデータを全件取得
    /// </summary>
    /// <returns></returns>
    public async ValueTask<IEnumerable<SalesDetailViewModel>> GetAllAsync()
    {
        var salesDetails = await _SalesDetailRepository.GetAllAsync();
        return salesDetails
            .Select(_mapper.Map<SalesDetailViewModel>)
            .ToArray();
    }

    public ValueTask RemoveAsync(int id)
    {
        return _SalesDetailRepository.RemoveAsync(id);
    }

    public ValueTask UpdateAsync(SalesDetailViewModel entity, int id)
    {
        return _SalesDetailRepository.UpdateAsync(_mapper.Map<SalesDetail>(entity), id);
    }

    public ValueTask UpdateAllAsync(IList<SalesDetailViewModel> entities, ISet<int> sets)
    {
        return _SalesDetailRepository.UpdateAllAsync(entities.Select(_mapper.Map<SalesDetail>), sets);
    }
}