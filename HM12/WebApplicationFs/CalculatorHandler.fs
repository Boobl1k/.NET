module WebApplicationFs.CalculatorHandler

open Giraffe.Core
open Giraffe
open WebApplication.Models

let CalculatorHttpHandler (calculator : ICachedCalculator, cache : ExpressionsCache) : HttpHandler =
    fun next ctx ->
        match ctx.GetQueryStringValue("expressionString") with
        | Ok str ->
            let res =
                CalculatorAdapter.calculate calculator cache str
            (setStatusCode 200 >=> json res) next ctx
        | Error e -> (setStatusCode 250 >=> json e) next ctx
