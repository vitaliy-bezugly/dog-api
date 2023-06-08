using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Common.Models;

public class DogSortingQueryBuilder
{
    public static IQueryable<Dog> TryBuildSortingQueryIfInvalidReturnSource(IQueryable<Dog> source, string attribute, string order)
    {
        var dictionary = new Dictionary<(string attribute, bool ascending), Func<IQueryable<Dog>, IQueryable<Dog>>>
        {
            { ("name", true), BuildQueryAsNameAscendingOrdering }, 
            { ("name", false), BuildQueryAsNameDescendingOrdering },
            { ("color", true), BuildQueryAsColorAscendingOrdering },
            { ("color", false), BuildQueryAsColorDescendingOrdering },
            { ("tailLength", true), BuildQueryAsTailLengthAscendingOrdering },
            { ("tailLength", false), BuildQueryAsTailLengthDescendingOrdering },
            { ("weight", true), BuildQueryAsWeightAscendingOrdering },
            { ("weight", false), BuildQueryAsWeightDescendingOrdering }
        };
        
        var ascendingOrder = order == "asc" || string.IsNullOrEmpty(order) || order == "ascending";
        if(dictionary.TryGetValue((attribute, ascendingOrder), out var func))
        {
            return func.Invoke(source);
        }
        
        return source;
    }
    
    private static IQueryable<Dog> BuildQueryAsNameAscendingOrdering(IQueryable<Dog> source)
    {
        return source.OrderBy(x => x.Name).AsQueryable();
    }
    
    private static IQueryable<Dog> BuildQueryAsNameDescendingOrdering(IQueryable<Dog> source)
    {
        return source.OrderByDescending(x => x.Name).AsQueryable();
    }
    
    private static IQueryable<Dog> BuildQueryAsColorAscendingOrdering(IQueryable<Dog> source)
    {
        return source.OrderBy(x => x.Color).AsQueryable();
    }
    
    private static IQueryable<Dog> BuildQueryAsColorDescendingOrdering(IQueryable<Dog> source)
    {
        return source.OrderByDescending(x => x.Color).AsQueryable();
    }
    
    private static IQueryable<Dog> BuildQueryAsTailLengthAscendingOrdering(IQueryable<Dog> source)
    {
        return source.OrderBy(x => x.TailLength).AsQueryable();
    }
    
    private static IQueryable<Dog> BuildQueryAsTailLengthDescendingOrdering(IQueryable<Dog> source)
    {
        return source.OrderByDescending(x => x.TailLength).AsQueryable();
    }
    
    private static IQueryable<Dog> BuildQueryAsWeightAscendingOrdering(IQueryable<Dog> source)
    {
        return source.OrderBy(x => x.Weight).AsQueryable();
    }
    
    private static IQueryable<Dog> BuildQueryAsWeightDescendingOrdering(IQueryable<Dog> source)
    {
        return source.OrderByDescending(x => x.Weight).AsQueryable();
    }
}