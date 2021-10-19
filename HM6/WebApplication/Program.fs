module WebApplication.Program

open System.IO
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting

[<EntryPoint>]
let main _ =
    let contentRoot = Directory.GetCurrentDirectory()
    let webRoot = Path.Combine(contentRoot, "WebRoot")

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
