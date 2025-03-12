using System.Linq.Expressions;

namespace SGH.Data.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> predicate)
    {
        return !condition ? source : source.Where<TSource>(predicate);
    }

    public static IQueryable<TSource> WhereIfElse<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> ifPredicate,
        Expression<Func<TSource, bool>> elsePredicate)
    {
        return !condition ? source.Where<TSource>(elsePredicate) : source.Where<TSource>(ifPredicate);
    }
}