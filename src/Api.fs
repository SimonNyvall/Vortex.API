module Vortex.Api

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http

type RequestResponse = HttpContext -> obj


/// <summary>
/// Maps a GET request to a specified pattern with a handler in a web application.
/// </summary>
/// <param name="pattern">The URL pattern to match for the GET request.</param>
/// <param name="handler">The function to handle the GET request.</param>
/// <param name="app">The instance of the WebApplication to configure.</param>
/// <returns>The configured WebApplication.</returns>
let mapGet (pattern: string) (handler: RequestResponse) (app: WebApplication) =
    app.MapGet(pattern, Func<HttpContext, obj> handler)
    |> ignore

    app


/// <summary>
/// Maps a POST request to a specified pattern with a handler in a web application.
/// </summary>
/// <param name="pattern">The URL pattern to match for the POST request.</param>
/// <param name="handler">The function to handle the POST request.</param>
/// <param name="app">The instance of the WebApplication to configure.</param>
/// <returns>The configured WebApplication.</returns>
let mapPost (pattern: string) (handler: RequestResponse) (app: WebApplication) =
    app.MapPost(pattern, Func<HttpContext, obj> handler)
    |> ignore

    app


/// <summary>
/// Maps a PUT request to a specified pattern with a handler in a web application.
/// </summary>
/// <param name="pattern">The URL pattern to match for the PUT request.</param>
/// <param name="handler">The function to handle the PUT request.</param>
/// <param name="app">The instance of the WebApplication to configure.</param>
/// <returns>The configured WebApplication.</returns>
let mapPut (pattern: string) (handler: RequestResponse) (app: WebApplication) =
    app.MapPut(pattern, Func<HttpContext, obj> handler)
    |> ignore

    app


/// <summary>
/// Maps a DELETE request to a specified pattern with a handler in a web application.
/// </summary>
/// <param name="pattern">The URL pattern to match for the DELETE request.</param>
/// <param name="handler">The function to handle the DELETE request.</param>
/// <param name="app">The instance of the WebApplication to configure.</param>
/// <returns>The configured WebApplication.</returns>
let mapDelete (pattern: string) (handler: RequestResponse) (app: WebApplication) =
    app.MapDelete(pattern, Func<HttpContext, obj> handler)
    |> ignore

    app

// TODO make a better folder strucutre
// TODO add json serialization for the api
