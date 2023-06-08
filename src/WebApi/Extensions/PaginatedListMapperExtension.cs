using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using WebApi.Contracts.Models;

namespace WebApi.Extensions;

public static class PaginatedListMapperExtension
{
    public static PaginatedList<DogViewModel> ToPaginationListViewModel(this IMapper mapper, PaginatedList<Dog> paginatedList)
    {
        var viewModels = paginatedList.Items.Select(mapper.Map<DogViewModel>).ToList();
        var listViewMode = new PaginatedList<DogViewModel>(viewModels, paginatedList.PageNumber, paginatedList.TotalPages, paginatedList.TotalCount);
        return listViewMode;;
    }
}