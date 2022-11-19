module Index

open Elmish
open Fable.Remoting.Client
open Feliz
open Shared


type Model = { User: User' option }

type Msg =
    | GotUser of User'

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init () : Model * Cmd<Msg> =
    let model = { User = None }

    let cmd = Cmd.OfAsync.perform todosApi.getUser () GotUser

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | GotUser user -> { model with User = Some user }, Cmd.none

open Feliz

let view (model: Model) (dispatch: Msg -> unit) =
    Interop.reactElement "div" []