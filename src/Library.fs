module Vortex.Api

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Vortex.Extensions.MinimalApiExtensions

let query (key: string) (context: HttpContext) : string = (string context.Request.Query.[key])

let tryQuery (key: string) (context: HttpContext) : string option =
    match context.Request.Query.TryGetValue(key) with
    | true, value -> Some(string value)
    | _ -> None

let tryQueryToType<'a> (key: string) (parse: string -> 'a) (context: HttpContext) : 'a option =
    match tryQuery key context with
    | Some value -> Some(parse value)
    | None -> None

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
