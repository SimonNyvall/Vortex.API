# Vortex.Api - F# Minimal API Wrapper [![.NET](https://github.com/SimonNyvall/Vortex/actions/workflows/dotnet.yml/badge.svg)](https://github.com/SimonNyvall/Vortex/actions/workflows/dotnet.yml)

<div align="center">
    <img src="./images/Vortex-logo.png" width="250">
</div>

## Overview

Vortex.Api provides a simple and effective way to configure routing for HTTP methods such as GET, POST, PUT, and DELETE within an F# application using the ASP.NET Core Minimal APIs. This module is designed to simplify the process of binding HTTP methods to specific request handlers.

## Installation

To use the `Vortex.Api` module, include it in your F# project by copying the source files directly or referencing the library if it's packaged. Ensure that your project references the `Microsoft.AspNetCore.App` framework.

## Usage

### Configuring the Web Application

Here's how to use the `Vortex.Api` to map different HTTP methods:

```fsharp
open Vortex.Api
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http

let getHandler: RequestHandler = fun _ -> 
        // Logic to handle the request
        "Hello, World!"

let configureApp (app: WebApplication) =
    app |> mapGet "/api/hello" getHandler
        |> mapPost "/api/post" postHandler
        |> mapPut "/api/put" putHandler
        |> mapDelete "/api/delete" delHandler

    app.Run()
```

## Vortex.Http - HttpContext Extensions
### Overview

Vortex.Http enhances the HttpContext by providing extension methods that facilitate easier access to services, request headers, cookies, and query parameters. It simplifies operations such as retrieving and deserializing request bodies and extracting service instances or loggers from the dependency injection container.

### Extension Methods
`NOTE`: The extension methods are available through the `Vortex.Http` namespace.

#### Logging and Services

- `GetLogger<'service>()`: Retrieves a logger service for the specified type, enabling easy logging within the request context.
- `GetService<'service>()`: Retrieves a service instance from the ASP.NET Core dependency injection container, allowing for decoupled and testable code.

#### Request Data

- `GetHeader(name: string)`: Returns the value of a specified request header, or an empty string if the header is not found.
- `GetCookie(key: string)`: Retrieves the value of a specified cookie, or `null` if the cookie does not exist.
- `GetBody<'body>()`: Deserializes the request body into a specified type. It throws an exception if deserialization fails.
- `TryGetBody<'body>()`: Tries to deserialize the request body into a specified type safely, returning `None` if unsuccessful to avoid exceptions.

#### Query Parameters

- `GetQuery(name: string)`: Retrieves the first value of a specified query parameter as a string, or an empty string if not present.
- `GetQueryInt(name: string)`: Retrieves the first value of a specified query parameter, converted to an integer. Throws a `FormatException` if the conversion fails.
- `GetQueryFloat(name: string)`: Retrieves the first value of a specified query parameter, converted to a float. Includes error handling for conversion failures.
- `GetQueryDecimal(name: string)`: Retrieves the first value of a specified query parameter, converted to a decimal. Error handling for conversion issues is included.
- `GetQueryDateTime(name: string)`: Converts the first value of a specified query parameter to a `DateTime` object. It throws a `FormatException` if the conversion is not possible.
- `GetQueryDateTimeOffset(name: string)`: Retrieves the first value of a specified query parameter and converts it to a `DateTimeOffset` object, with error handling for conversion failures.
- `GetQueryGuid(name: string)`: Retrieves and converts the first value of a specified query parameter to a `Guid`.
- `GetQueryUri(name: string)`: Converts the first value of a specified query parameter to a `Uri` object. Includes handling for `UriFormatException`.
- `GetQueryList(name: string)`: Retrieves all values of a specified query parameter as a list of strings, allowing for multiple values per query parameter.

### Usage
Here are a few examples of how Vortex.Http can be used within your ASP.NET Core handlers:
    
```fsharp
open Vortex.Http
open Microsoft.AspNetCore.Http

let exampleHandler: RequestHandler = fun context -> // RequestHandler is a type alias for HttpContext -> obj
    let logger = context.GetLogger<YourServiceType>()
    let service = context.GetService<YourServiceType>()
    let headerValue = context.GetHeader "User-Agent"

    // Log and return the header value
    logger.LogInformation("Received user-agent: {HeaderValue}", headerValue)

    headerValue
```

## Installation
Add the Vortex.Api package to your project using the following command:

``` sh
dotnet add package Vortex.Api --version 0.1.0
```

## Contributing
Contributions are welcome! Please refer to the TODO.md or create an issue to get started.

## License
This project is licensed under the MIT License - see the LICENSE file for details.
