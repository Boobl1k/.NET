namespace WebApplication

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

type Message = { Text: string }

module Views =
    open Giraffe.ViewEngine

    let layout (content: XmlNode list) =
        html [] [
            head [] [
                title [] [ encodedText "ЖЫРАФ" ]
                link [ _rel "stylesheet"
                       _type "text/css"
                       _href "main.css" ]
            ]
            body [] content
        ]

    let partial () = h1 [] [ encodedText "ЖЫРАФ" ]

    let index (model: Message) =
        [ partial ()
          p [] [ encodedText model.Text ] ]
        |> layout

module Startup =
    open Giraffe.ViewEngine
    
    let indexHandler (name: string) =
        let greetings = $"Hello {name}, from Giraffe!"
        let model = { Text = greetings }
        let view = Views.index model
        htmlView view
    let webApp =
        choose [
            route "/bod"   >=> text "bod bod bod"
            route "/" >=> indexHandler "world" ]
    
    type Startup() =
            member _.ConfigureServices (services : IServiceCollection) =
        // Register default Giraffe dependencies
                services.AddGiraffe() |> ignore

            member _.Configure (app : IApplicationBuilder)
                            (env : IHostEnvironment)
                            (loggerFactory : ILoggerFactory) =
        // Add Giraffe to the ASP.NET Core pipeline
                app
                    .UseStaticFiles()
                    .UseGiraffe(webApp)
