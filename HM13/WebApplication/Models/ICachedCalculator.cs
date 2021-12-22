using System.Linq.Expressions;

namespace WebApplication.Models;

public interface ICachedCalculator
{
    Expression FromString(string str);
    decimal CalculateWithCache(Expression expression, ExpressionsCache cache);
}
