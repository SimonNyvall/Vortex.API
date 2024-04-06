namespace Vortex.Tests

open System
open Vortex.Api
open Vortex.Http
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http


module Tests =


    [<EntryPoint>]
    let main argv =
        let builder = WebApplication.CreateBuilder(argv)

        let app = builder.Build()

        let handler: RequestResponse = fun _ -> Results.Ok("Hello world")

        // get simple hello world test
        app |> mapGet "/test" handler |> ignore

        // get query test
        app
        |> mapGet "/query" (fun context ->
            let id = context.GetQueryInt "id"

            Results.Ok(id))
        |> ignore

        app
        |> mapGet "/async" (fun context -> async { return Results.Ok(id) })
        |> ignore

        app.Run()

        0
