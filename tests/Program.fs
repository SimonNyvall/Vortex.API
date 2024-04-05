namespace Vortex.Tests

open System
open Vortex.Api
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http


module Tests =


    [<EntryPoint>]
    let main argv =
        let builder = WebApplication.CreateBuilder(argv)

        let app = builder.Build()

        let handler context : HttpContext -> obj = Results.Ok("Hello world")

        // get simple hello world test
        app |> mapGet "/test" handler |> ignore

        // get query test
        app
        |> mapGet "/query" (fun context ->
            let id = context |> query "id"

            Results.Ok(id))
        |> ignore

        app.Run()

        0
