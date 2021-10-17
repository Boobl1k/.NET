module WebApplication.Startup

open Views
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open WebApplication
open CalculatorHandler

let indexHandler (name: string) =
    let greetings = $"Hello {name}, from Giraffe!"
    let model = { Text = greetings }
    let view = Views.index model
    htmlView view

let webApp =
    let sum = "sum"
    let mult = "mult"
    let dif = "dif"
    let div = "div"
    choose [ route "/bod" >=> text "bod bod bod"
             route "/" >=> indexHandler "world"
             route $"/{sum}" >=> CalculatorHttpHandler sum
             route $"/{mult}" >=> CalculatorHttpHandler mult
             route $"/{dif}" >=> CalculatorHttpHandler dif
             route $"/{div}" >=> CalculatorHttpHandler div]

type Startup() =
    member _.ConfigureServices(services: IServiceCollection) =
        // Register default Giraffe dependencies
        services.AddGiraffe() |> ignore

    member _.Configure (app: IApplicationBuilder) (env: IHostEnvironment) (loggerFactory: ILoggerFactory) =
        // Add Giraffe to the ASP.NET Core pipeline
        app.UseStaticFiles().UseGiraffe(webApp)
