using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.DB;

namespace WebApplication.Models;

public class ExpressionsCache
{
    private static readonly List<ComputedExpression> cache = new();

    //public ExpressionsCache(IDbContext<ComputedExpression> context) =>
    //_context = context;

    public ComputedExpression GetOrSet(
        ComputedExpression expWithoutRes,
        Func<decimal> resultBuilder)
    {
        try
        {
            lock (cache)
            {
                //TODO воняет слегка
                return cache.First(expression =>
                    expression.V1 == expWithoutRes.V1 &&
                    expression.V2 == expWithoutRes.V2 &&
                    expression.Op == expWithoutRes.Op);
            }
        }
        catch
        {
            expWithoutRes.Res = resultBuilder();
            lock (cache)
            {
                cache.Add(expWithoutRes);
            }

            return expWithoutRes;
        }
    }
}
