using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.DB;

namespace WebApplication.Models;

public class ExpressionsCache
{
    private readonly List<ComputedExpression> _context = new();

    //public ExpressionsCache(IDbContext<ComputedExpression> context) =>
        //_context = context;

    public ComputedExpression GetOrSet(
        ComputedExpression expWithoutRes,
        Func<decimal> resultBuilder)
    {
        try
        {
            lock (_context)
            {
                return _context.First(expression =>
                    expression.V1 == expWithoutRes.V1 &&
                    expression.V2 == expWithoutRes.V2 &&
                    expression.Op == expWithoutRes.Op);
            }
        }
        catch
        {
            expWithoutRes.Res = resultBuilder();
            lock (_context)
            {
                _context.Add(expWithoutRes);
            }
            return expWithoutRes;
        }
    }

    public void SaveChanges()
    {
        
    }
        //_context.SaveChanges();
}
