namespace Vortex.Tests

module HttpContextExtensionsTests =

    open System
    open Xunit
    open Microsoft.AspNetCore.Http
    open Microsoft.Extensions.DependencyInjection
    open Microsoft.Extensions.Logging
    open NSubstitute
    open Vortex.Http

    type IService =
        interface
        end

    type Body =
        { Id: int }

    type HttpContextExtensionsTests() =

        [<Fact>]
        member _.``GetLogger returns exspected logger``() =
            // Arrange
            let services = new ServiceCollection()
            let logger = Substitute.For<ILogger<HttpContextExtensionsTests>>()

            services.AddSingleton(logger) |> ignore

            let serviceProvider = services.BuildServiceProvider()

            let httpContext = Substitute.For<HttpContext>()

            httpContext.RequestServices.Returns(serviceProvider)
            |> ignore

            // Act
            let result = httpContext.GetLogger<HttpContextExtensionsTests>()

            // Assert
            Assert.Same(logger, result)

        [<Fact>]
        member _.``GetService returns exspected service``() =
            // Arrange
            let services = new ServiceCollection()
            let service = Substitute.For<IService>()
            services.AddSingleton(service) |> ignore
 
            let serviceProvider = services.BuildServiceProvider()

            let httpContext = Substitute.For<HttpContext>()

            httpContext.RequestServices.Returns(serviceProvider)
            |> ignore

            // Act
            let result = httpContext.GetService<IService>()

            // Assert
            Assert.Same(service, result)

        [<Fact>]
        member _.``GetHeader returns exspected header``() =
            // Arrange
            let httpContext = Substitute.For<HttpContext>()
            let headers = new HeaderDictionary()
            headers.Add("key", "value")

            httpContext.Request.Headers.Returns(headers)
            |> ignore

            // Act
            let result = httpContext.GetHeader("key")

            // Assert
            Assert.Equal("value", result)

        [<Fact>]
        member _.``GetCookie returns exspected cookie``() =
            // Arrange
            let httpContext = Substitute.For<HttpContext>()
            let cookies = Substitute.For<IRequestCookieCollection>()
            cookies.Item("key").Returns("value") |> ignore

            httpContext.Request.Cookies.Returns(cookies)
            |> ignore

            // Act
            let result = httpContext.GetCookie("key")

            // Assert
            Assert.Equal("value", result)
(* // TODO Something wrong with this test.
    [<Fact>]
    member _.``GetBody returns empty string when body is empty``() =
        // Arrange
        let httpContext = Substitute.For<HttpContext>()
        let body = new MemoryStream()

        httpContext.Request.Body.Returns(body)
        |> ignore

        // Act
        let result = httpContext.GetBody<Body>()

        // Assert
        Assert.Equal(0, result.Id)
*)
        [<Fact>]
        member _.``GetQuery returns expected query string``() =
            // Arrange
            let queryCollection = Substitute.For<IQueryCollection>()
            queryCollection.Item("key").Returns("value") |> ignore

            let httpContext = Substitute.For<HttpContext>()
            httpContext.Request.Query.Returns(queryCollection)
            |> ignore

            // Act
            let result = 
                let returnQuery = httpContext.GetQuery("key")

                match returnQuery.Count with
                | 0 -> "Test Failed"
                | _ -> returnQuery.Item(0)

            // Assert
            Assert.Equal("value", result)

        [<Fact>]
        member _.``GetQueryList returns expected query list``() =
            // Arrange
            let queryCollection = Substitute.For<IQueryCollection>()
            queryCollection.Item("key").Returns([| "1" ; "2" |]) |> ignore 

            let httpContext = Substitute.For<HttpContext>()
            httpContext.Request.Query.Returns(queryCollection)
            |> ignore

            // Act
            let result = httpContext.GetQueryList("key")

            // Assert
            Assert.Equal([| "1"; "2" |], result)

        [<Fact>]
        member _.``GetQueryInt returns exspected query int``() =
            // Arrange
            let queryCollection = Substitute.For<IQueryCollection>()
            queryCollection.Item("key").Returns("1") |> ignore

            let httpContext = Substitute.For<HttpContext>()
            httpContext.Request.Query.Returns(queryCollection)
            |> ignore

            // Act
            let result = httpContext.GetQueryInt("key")

            // Assert
            Assert.Equal(1, result)

        [<Fact>]
        member _.``GetQueryFloat returns exspected query float``() =
            // Arrange
            let queryCollection = Substitute.For<IQueryCollection>()
            queryCollection.Item("key").Returns("1.1") |> ignore

            let httpContext = Substitute.For<HttpContext>()
            httpContext.Request.Query.Returns(queryCollection)
            |> ignore

            // Act
            let result = httpContext.GetQueryFloat("key")

            // Assert
            Assert.Equal(1.1, result)

        [<Fact>]
        member _.``GetQueryDecimal returns exspected query decimal``() =
            // Arrange
            let queryCollection = Substitute.For<IQueryCollection>()
            queryCollection.Item("key").Returns("1.1") |> ignore

            let httpContext = Substitute.For<HttpContext>()
            httpContext.Request.Query.Returns(queryCollection)
            |> ignore

            // Act
            let result = httpContext.GetQueryDecimal("key")

            // Assert
            Assert.Equal(1.1M, result)

        [<Fact>]
        member _.``GetQueryDateTime returns exspected query datetime``() =
            // Arrange
            let queryCollection = Substitute.For<IQueryCollection>()
            queryCollection.Item("key").Returns("2020-01-01T00:00:00") |> ignore

            let httpContext = Substitute.For<HttpContext>()
            httpContext.Request.Query.Returns(queryCollection)
            |> ignore

            // Act
            let result = httpContext.GetQueryDateTime("key")

            // Assert
            Assert.Equal(DateTime(2020, 1, 1), result)

        [<Fact>]
        member _.``GetQueryDateTimeOffset returns exspected query datetimeoffset``() =
            // Arrange
            let queryCollection = Substitute.For<IQueryCollection>()
            queryCollection.Item("key").Returns("2020-01-01T00:00:00Z") |> ignore

            let httpContext = Substitute.For<HttpContext>()
            httpContext.Request.Query.Returns(queryCollection)
            |> ignore

            // Act
            let result = httpContext.GetQueryDateTimeOffset("key")

            // Assert
            Assert.Equal(DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero), result)

        [<Fact>]
        member _.``GetQueryGuid returns exspected query guid``() =
            // Arrange
            let queryCollection = Substitute.For<IQueryCollection>()
            queryCollection.Item("key").Returns("00000000-0000-0000-0000-000000000000") |> ignore

            let httpContext = Substitute.For<HttpContext>()
            httpContext.Request.Query.Returns(queryCollection)
            |> ignore

            // Act
            let result = httpContext.GetQueryGuid("key")

            // Assert
            Assert.Equal(Guid.Empty, result)

        [<Fact>]
        member _.``GetQueryList returns exspected query list``() =
            // Arrange
            let queryCollection = Substitute.For<IQueryCollection>()
            queryCollection.Item("key").Returns([| "1" ; "2" |]) |> ignore

            let httpContext = Substitute.For<HttpContext>()
            httpContext.Request.Query.Returns(queryCollection)
            |> ignore

            // Act
            let result = httpContext.GetQueryList("key")

            // Assert
            Assert.Equal([| "1"; "2" |], result)
            Assert.IsType<string list>(result)
