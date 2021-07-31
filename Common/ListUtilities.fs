module ListUtilities

let rec distribute e = function
| [] -> [[e]]
| x::xs' as xs -> (e::xs)::[for xs in distribute e xs' -> x::xs]

let rec permute = function
| [] -> [[]]
| e::xs -> List.collect (distribute e) (permute xs)

let collectByFirst (input:string list) : string list list =
    let rec loop (rest:string list) (acc:string list list) =
        match rest with
        | [] -> acc |> List.rev
        | _ when rest.Length = 1 -> acc |> List.rev
        | _::xs -> loop xs (rest::acc)
    loop input []