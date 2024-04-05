module Vortex.Api

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http

type RequestResponse = HttpContext -> obj

let mapGet (pattern: string) (handler: RequestResponse) (app: WebApplication) =
    app.MapGet(pattern, Func<HttpContext, obj> handler) |> ignore

    app

let mapPost (pattern: string) (handler: RequestResponse) (app: WebApplication) =
    app.MapPost(pattern, Func<HttpContext, obj> handler) |> ignore

    app

let mapPut (pattern: string) (handler: RequestResponse) (app: WebApplication) =
    app.MapPut(pattern, Func<HttpContext, obj> handler) |> ignore

    app

let mapDelete (pattern: string) (handler: RequestResponse) (app: WebApplication) =
    app.MapDelete(pattern, Func<HttpContext, obj> handler) |> ignore

    app

// TODO add a type that works like HttpContext -> obj
// TODO allow for async functions
// TODO make a better folder strucutre
// TODO add a way to get the body of a Request
// TODO add a way to get the headers of a Request
// TODO add a way to get the cookies of a Request
// TODO add a way to get the session of a Request
// TODO add a way to get the user of a Request
// TODO add a way to get the user claims of a Request
// TODO add a way to get the user roles of a Request
// TODO add a way to get the user permissions of a Request
// TODO add a way to get the user policies of a Request
// TODO add json serialization for the api
