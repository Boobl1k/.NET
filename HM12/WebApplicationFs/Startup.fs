namespace WebApplicationFs

open Views
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open CalculatorHandler
open WebApplication.DB
open WebApplication.Models

module private StartupUtil = 
    let indexHandler (name: string) =
        let greetings = $"Hello {name}, from Giraffe!"
        let model = { Text = greetings }
        let view = Views.index model
        htmlView view

    let webApp =
        choose [
            GET >=> choose [
                route "/bod" >=> text "bod bod bod"
                route "/" >=> htmlFile "WebRoot/pages/index.html"
                route "/calc" >=> CalculatorHttpHandler(CachedCalculator(Calculator()), ExpressionsCache())
                ]
            ]
        
open StartupUtil
type Startup() =
    member _.ConfigureServices(services: IServiceCollection) =
        // Register default Giraffe dependencies
        services.AddGiraffe() |> ignore

    member _.Configure (app: IApplicationBuilder) (env: IHostEnvironment) (loggerFactory: ILoggerFactory) =
        // Add Giraffe to the ASP.NET Core pipeline
        app.UseStaticFiles().UseGiraffe(webApp)
