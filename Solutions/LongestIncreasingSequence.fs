module LongestIncreasingSequence

let isIncrease x y = y > x

let isDecrease x y = x > y

let findSub criteria input =
    match input with
    | [] -> []
    | x::xs -> 
        let rec loop rest result =
            match rest with
            | [] -> result |> List.rev
            | x::xs -> 
                match criteria (result |> List.head) x with
                | true -> loop xs (x::result)
                | false -> loop xs result
        loop xs [x]
    
let getResult result =
    result
    |> List.sortBy (fun i -> i |> List.length)
    |> List.rev
    |> List.head

let findAllSubs criteria input =
    let rec loop rest result =
        let seq = findSub criteria rest
        match rest with
        | [] ->
            match seq with
            | [] -> getResult result
            | _ -> getResult (seq::result)
        | _::xs ->
            match seq with
            | [] -> loop xs result
            | _ -> loop xs (seq::result)
    loop input []
    