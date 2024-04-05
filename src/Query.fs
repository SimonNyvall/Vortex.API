moudle Vortex.Api.Query

let query (key: string) (context: HttpContext) : string = (string context.Request.Query.[key])

let tryQuery key (context: HttpContext) : string option =
    match context.Request.Query.TryGetValue(key) with
    | true, value -> Some(string value)
    | _ -> None

let tryQueryToType<'a> key parse context : 'a option =
    tryQuery key context
    |> Option.bind (fun value ->
        try
            Some(parse value)
        with _ ->
            None)
