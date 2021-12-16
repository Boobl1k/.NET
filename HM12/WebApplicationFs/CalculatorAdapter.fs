module WebApplicationFs.CalculatorAdapter

open Microsoft.FSharp.Core
open WebApplication.Models

let calculate (calculator: ICachedCalculator) (cache : ExpressionsCache) str : decimal =
    let str = ExpressionStringFix.Fix(str)
    let expression = calculator.FromString(str)
    calculator.CalculateWithCache(expression, cache)
