module Tests

open System
open Xunit
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open System.Net.Http
open Vortex.Api

let private usedPorts = ref []

let rec private getPort () =
    let random = new Random()
    let port = random.Next(5000, 6000)

    match List.contains port usedPorts.Value with
    | true -> getPort ()
    | false ->
        usedPorts.Value <- port :: usedPorts.Value
        port


[<Fact>]
let ``Test GET Handler Returns Expected Response`` () =
    let builder = WebApplication.CreateBuilder()

    let app = builder.Build()

    let expectedResponse = "Hello, World!"

    let port = getPort ()
    app.Urls.Add($"http://localhost:{port}")

    let handler: RequestResponse = fun _ -> expectedResponse

    app |> mapGet "/test/helloworld" handler |> ignore

    app.Start()

    use client = new HttpClient(BaseAddress = new Uri($"http://localhost:{port}"))

    async {
        let! response =
            client.GetAsync($"http://localhost:{port}/test/helloworld")
            |> Async.AwaitTask

        let! content =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        Assert.Equal(expectedResponse, content)
    }
    |> Async.RunSynchronously

    app.DisposeAsync


[<Fact>]
let ``Test POST Handler Returns Expected Response`` () =
    let builder = WebApplication.CreateBuilder()

    let app = builder.Build()

    let expectedResponse = "Hello, World!"
    let port = getPort ()
    app.Urls.Add($"http://localhost:{port}")

    let handler: RequestResponse = fun _ -> expectedResponse

    app
    |> mapPost "/test/helloworld" handler
    |> ignore

    app.Start()

    use client = new HttpClient(BaseAddress = new Uri($"http://localhost:{port}"))

    async {
        let! response =
            client.PostAsync($"http://localhost:{port}/test/helloworld", new StringContent(""))
            |> Async.AwaitTask

        let! content =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        Assert.Equal(expectedResponse, content)
    }
    |> Async.RunSynchronously

    app.DisposeAsync


[<Fact>]
let ``Test PUT Handler Returns Expected Response`` () =
    let builder = WebApplication.CreateBuilder()

    let app = builder.Build()

    let expectedResponse = "Hello, World!"

    let port = getPort ()
    app.Urls.Add($"http://localhost:{port}")

    let handler: RequestResponse = fun _ -> expectedResponse

    app |> mapPut "/test/helloworld" handler |> ignore

    app.Start()

    use client = new HttpClient(BaseAddress = new Uri($"http://localhost:{port}"))

    async {
        let! response =
            client.PutAsync($"http://localhost:{port}/test/helloworld", new StringContent(""))
            |> Async.AwaitTask

        let! content =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        Assert.Equal(expectedResponse, content)
    }
    |> Async.RunSynchronously

    app.DisposeAsync


[<Fact>]
let ``Test DELETE Handler Returns Expected Response`` () =
    let builder = WebApplication.CreateBuilder()

    let app = builder.Build()

    let expectedResponse = "Hello, World!"

    let port = getPort ()
    app.Urls.Add($"http://localhost:{port}")

    let handler: RequestResponse = fun _ -> expectedResponse

    app
    |> mapDelete "/test/helloworld" handler
    |> ignore

    app.Start()

    use client = new HttpClient(BaseAddress = new Uri($"http://localhost:{port}"))

    async {
        let! response =
            client.DeleteAsync($"http://localhost:{port}/test/helloworld")
            |> Async.AwaitTask

        let! content =
            response.Content.ReadAsStringAsync()
            |> Async.AwaitTask

        Assert.Equal(expectedResponse, content)
    }
    |> Async.RunSynchronously

    app.DisposeAsync
