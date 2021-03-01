module LongestIncreasingSequence

let isIncrease x y = y > x

let isDecrease x y = x > y

let findFirstSubSeq criteria seq start =
    let rec loop rest result =
        match rest with
        | [] -> result |> List.rev
        | x::xs -> 
            match criteria (result |> List.head) x with
            | true -> loop xs (x::result)
            | false -> loop xs result
    loop seq [start]
   
let getResult result =
    result
    |> List.sortBy (fun i -> i |> List.length)
    |> List.rev
    |> List.head

let findLongestSub criteria input =
    let start = input |> List.head
    let rec loop rest result =
        match rest with
        | [] -> getResult result
        | _::ys ->
            let subSeq = findFirstSubSeq criteria rest start
            loop ys (subSeq::result)
    loop (input |> List.tail) []

type LongSubSampleData = {count:int; sequence:int list}

