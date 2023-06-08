using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Contracts.Queries;

public class PaginationQuery
{
    [FromQuery(Name = "pageNumber")] 
    public int PageNumber { get; set; } = 1;
    [FromQuery(Name = "pageSize")] 
    public int PageSize { get; set; } = 50;
}