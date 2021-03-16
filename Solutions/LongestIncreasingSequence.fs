module LongestIncreasingSequence

open System.Text
open FParsec

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

let findLongestSubConstantStart criteria start input =
    let rec loop rest result =
        match rest with
        | [] -> getResult result
        | _::ys ->
            let subSeq = findFirstSubSeq criteria rest start
            loop ys (subSeq::result)
    loop input []    

let findLongestSub criteria input =
    let rec loop rest acc =
        match rest with
        | [] -> getResult acc
        | x::xs ->
            match xs with
            | [] -> loop xs acc
            | _ ->
                let seq = findLongestSubConstantStart criteria x xs
                loop xs (seq::acc)
    loop input []

type LongSubSampleData = {count:int; sequence:int list}

let longSubSampleParser = 
    let countParser = pint32 .>> newline
    let seqParser = many (spaces >>? pint32)
    tuple2 countParser seqParser   

let parseSingleEntry input =
    let reply = run longSubSampleParser input
    match reply with
    | Success(result,_,_) -> result |> (fun r -> {count = fst(r); sequence = snd(r)})
    | Failure(_,_,_) -> {count = 0; sequence = []}
    
let parseSampleEntriesFile path =
    let entries = many1 longSubSampleParser
    let reply = runParserOnFile entries () path Encoding.UTF8
    match reply with
    | Success(result,_,_) -> result |> List.map (fun r -> {count = fst(r); sequence = snd(r)})
    | Failure(_,_,_) -> List.empty
         