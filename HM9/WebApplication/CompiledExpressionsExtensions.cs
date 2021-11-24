using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace WebApplication
{
    internal static class CompiledExpressionsExtensions
    {
        public static Func<TOut> AsFunc<TOut>(this Expression<Func<TOut>> expression) =>
            CompiledExpressions<TOut>.AsFunc(expression);

        private static class CompiledExpressions<TOut>
        {
            private static readonly ConcurrentDictionary<Expression<Func<TOut>>, Func<TOut>> cache = new();

            public static Func<TOut> AsFunc(Expression<Func<TOut>> expression) =>
                cache.GetOrAdd(expression, e => e.Compile());
        }
        
        public static Func<TIn, TOut> AsFunc<TIn, TOut>(this Expression<Func<TIn, TOut>> expression) =>
            CompiledExpressions<TIn, TOut>.AsFunc(expression);

        private static class CompiledExpressions<TIn, TOut>
        {
            private static readonly ConcurrentDictionary<Expression<Func<TIn, TOut>>, Func<TIn, TOut>> cache = new();

            public static Func<TIn, TOut> AsFunc(Expression<Func<TIn, TOut>> expression) =>
                cache.GetOrAdd(expression, e => e.Compile());
        }
    }
}
