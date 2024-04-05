module Vortex.Extensions.MinimalApiExtensions

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http

let mapGet<'returnType> (pattern: string) (func: HttpContext -> 'returnType) (app: WebApplication) =
    app.MapGet(pattern, Func<HttpContext, 'returnType> func) |> ignore

    app

let mapPost<'returnType> (pattern: string) (func: HttpContext -> 'returnType) (app: WebApplication) =
    app.MapPost(pattern, Func<HttpContext, 'returnType> func) |> ignore

    app

let mapPut<'returnType> (pattern: string) (func: HttpContext -> 'returnType) (app: WebApplication) =
    app.MapPut(pattern, Func<HttpContext, 'returnType> func) |> ignore

    app

let mapDelete<'returnType> (pattern: string) (func: HttpContext -> 'returnType) (app: WebApplication) =
    app.MapDelete(pattern, Func<HttpContext, 'returnType> func) |> ignore

    app
