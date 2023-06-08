using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Contracts.Queries;

public class PaginationQuery
{
    [FromQuery(Name = "pageNumber"), Range(1, int.MaxValue)] 
    public int PageNumber { get; set; } = 1;
    [FromQuery(Name = "pageSize"), Range(1, 50)] 
    public int PageSize { get; set; } = 50;
}