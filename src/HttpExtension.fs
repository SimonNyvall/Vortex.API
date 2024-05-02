module Vortex.Http

open System
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging

/// Extension methods for HttpContext.
type HttpContext with

    /// <summary>
    /// Retrieves a logger service for the specified type.
    /// </summary>
    /// <returns>The logger instance for the specified service type.</returns>
    member this.GetLogger<'service>() =
        this.RequestServices.GetService<ILogger<'service>>()


    /// <summary>
    /// Retrieves a service from the DI container.
    /// </summary>
    /// <returns>The service instance of the specified type.</returns>
    member this.GetService<'service>() =
        this.RequestServices.GetService<'service>()


    /// <summary>
    /// Retrieves the value of a specified request header.
    /// </summary>
    /// <param name="name">The name of the header.</param>
    /// <returns>The value of the header if exists, otherwise StringValues.Empty.</returns>
    member this.GetHeader(name: string) = this.Request.Headers.[name]


    /// <summary>
    /// Retrieves the value of a specified cookie.
    /// </summary>
    /// <param key="key">The key of the cookie.</param>
    /// <returns>The value of the cookie if it exists, otherwise null.</returns>
    member this.GetCookie(key: string) = this.Request.Cookies.[key]


    /// <summary>
    /// Deserializes and returns the request body as the specified type.
    /// </summary>
    /// <remarks>
    /// The request body can only be read once, so calling this method multiple times may not work as expected unless the request stream is reset.
    /// </remarks>
    /// <returns>The deserialized object from the request body.</returns>
    /// <exception cref="System.Text.Json.JsonException">Thrown when the request body cannot be deserialized into the specified type.</exception>
    member this.GetBody<'body>() =
        let body = this.Request.Body

        use reader = new System.IO.StreamReader(body)
        let bodyString = reader.ReadToEnd()
        System.Text.Json.JsonSerializer.Deserialize<'body>(bodyString)


    /// <summary>
    /// Attempts to deserialize and return the request body as the specified type.
    /// </summary>
    /// <remarks>
    /// This method attempts to read and deserialize the request body. If the operation fails, None is returned instead of throwing an exception.
    /// </remarks>
    /// <returns>An option containing the deserialized object from the request body if successful, otherwise None.</returns>
    member this.TryGetBody<'body>() =
        try
            Some(this.GetBody<'body>())
        with
        | _ -> None


    /// <summary>
    /// Retrieves the first value of a specified query parameter as a string.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <returns>The first value of the query parameter if it exists, otherwise StringValues.Empty.</returns>
    member this.GetQuery(name: string) = this.Request.Query.[name]


    /// <summary>
    /// Retrieves the first value of a specified query parameter as an integer.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <returns>The first value of the query parameter converted to an integer.</returns>
    /// <exception cref="System.FormatException">Thrown if the value cannot be converted to an integer.</exception>
    member this.GetQueryInt(name: string) =
        this.Request.Query.[name] |> Seq.head |> int


    /// <summary>
    /// Retrieves the first value of a specified query parameter as a floating-point number.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <returns>The first value of the query parameter converted to a float.</returns>
    /// <exception cref="System.FormatException">Thrown if the value cannot be converted to a float.</exception>
    member this.GetQueryFloat(name: string) =
        this.Request.Query.[name] |> Seq.head |> float


    /// <summary>
    /// Retrieves the first value of a specified query parameter as a decimal.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <returns>The first value of the query parameter converted to a decimal.</returns>
    /// <exception cref="System.FormatException">Thrown if the value cannot be converted to a decimal.</exception>
    member this.GetQueryDecimal<'types>(name: string) =
        this.Request.Query.[name] |> Seq.head |> decimal


    /// <summary>
    /// Retrieves the first value of a specified query parameter as a DateTime object.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <returns>The first value of the query parameter converted to a DateTime object.</returns>
    /// <exception cref="System.FormatException">Thrown if the value cannot be converted to a DateTime object.</exception>
    member this.GetQueryDateTime(name: string) =
        this.Request.Query.[name]
        |> Seq.head
        |> DateTime.Parse


    /// <summary>
    /// Retrieves the first value of a specified query parameter as a DateTimeOffset object.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <returns>The first value of the query parameter converted to a DateTimeOffset object.</returns>
    /// <exception cref="System.FormatException">Thrown if the value cannot be converted to a DateTimeOffset object.</exception>
    member this.GetQueryDateTimeOffset(name: string) =
        this.Request.Query.[name]
        |> Seq.head
        |> DateTimeOffset.Parse


    /// <summary>
    /// Retrieves the first value of a specified query parameter as a Guid.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <returns>The first value of the query parameter converted to a Guid.</returns>
    /// <exception cref="System.FormatException">Thrown if the value cannot be converted to a Guid.</exception>
    member this.GetQueryGuid(name: string) =
        this.Request.Query.[name]
        |> Seq.head
        |> Guid.Parse


    /// <summary>
    /// Retrieves the first value of a specified query parameter as a Uri object.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <returns>The first value of the query parameter converted to a Uri object.</returns>
    /// <exception cref="System.UriFormatException">Thrown if the value cannot be converted to a Uri object.</exception>
    member this.GetQueryUri(name: string) =
        this.Request.Query.[name] |> Seq.head |> Uri

    /// <summary>
    /// Retrieves all values of a specified query parameter as a list of strings.
    /// </summary>
    /// <param name="name">The name of the query parameter.</param>
    /// <returns>A list containing all values of the specified query parameter.</returns>
    member this.GetQueryList(name: string) = this.Request.Query.[name] |> Seq.toList
