module WebApplication.Program

open System.IO
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting

let contentRoot = Directory.GetCurrentDirectory()
let webRoot = Path.Combine(contentRoot, "WebRoot")

let CreateHostBuilder (_ : string array) =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun webHostBuilder ->
            webHostBuilder
                .UseContentRoot(contentRoot)
                .UseWebRoot(webRoot)
                .UseStartup<Startup>()
            |> ignore)


[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun webHostBuilder ->
            webHostBuilder
                .UseContentRoot(contentRoot)
                .UseWebRoot(webRoot)
                .UseStartup<Startup>()
            |> ignore)
        .Build()
        .Run()

    0
