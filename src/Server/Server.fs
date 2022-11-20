module Server

open System
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Microsoft.AspNetCore.Http
open Saturn
open Shared

let todosApi =
    {
      getUser = fun() -> async { return { Type = Tag } }
    }

let errorHandler (ex: Exception) (routeInfo: RouteInfo<HttpContext>) =
    Propagate { error = ex.ToString()
                ignored = false
                handled = true }

let webApp =
    Remoting.createApi ()
    |> Remoting.withErrorHandler errorHandler
    |> Remoting.withDiagnosticsLogger (printfn "%s")
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromValue todosApi
    |> Remoting.buildHttpHandler

let app =
    application {
        use_router webApp
        memory_cache
        use_static "public"
        use_gzip
    }

[<EntryPoint>]
let main _ =
    run app
    0