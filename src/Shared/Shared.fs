namespace Shared

type ContentType' = Tag | Attribute | Text

type User' = {
    Id: string
    UserName: string
    Sites: Site' list
}
and Site' = {
    Id: int
    DomainName: string
    Pages: Page' list
}
and Page' = {
    Id: int
    SiteId: int
    Uri: string
    Title: string
    Description: string
    Keywords: seq<string>
    Contents: Content' list
}
and Content' = {
    Id: int
    Position: int
    Type: ContentType'
    PageId: int option
    Children: Content' list
    ParentId: int option
    Value: string option
    Name: string
    CssId: string option
    CssClass: string option
    IsTemplate: bool
    IsEditable: bool
}

module Route =
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

type ITodosApi =
    { getUser: unit -> Async<User'> }