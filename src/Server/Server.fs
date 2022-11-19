module Server

open System
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Microsoft.AspNetCore.Http
open Saturn

open Shared


let todosApi =
    {
      getUser =
          fun() -> async {
              return {
                Id = "99c19cba-dbce-4359-8be2-edb1892db452"
                UserName = "test@redx.su"
                Sites =
                [
                    {
                        Id = 2
                        DomainName = "test.com"
                        Pages = [
                            {
                                Id = 2
                                SiteId = 2
                                Uri = "/"
                                Title = "bla"
                                Description = ""
                                Keywords = seq []
                                Contents = [
                                    {
                                        Id = 1270
                                        Position = 0
                                        Type = Tag
                                        PageId = Some 2
                                        Children = []
                                        ParentId = None
                                        Value = None
                                        Name = "html"
                                        CssId = None
                                        CssClass = None
                                        IsTemplate = false
                                        IsEditable = false
                                    }
                                ]
                            }
                        ]
                    }
                ]
                }
          }
    }
let errorHandler (ex: Exception) (routeInfo: RouteInfo<HttpContext>) =
    match ex with
    | :? Exception as ex ->
        let customError = {
            error = ex.ToString()
            ignored = false
            handled = true }
        Propagate customError
    | _ -> Ignore // ignore error
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