namespace Shared


type UserType' = Tag | Member | Admin

type User' = {
    Type: UserType'
}

module Route =
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

type ITodosApi =
    { getUser: unit -> Async<User'> }