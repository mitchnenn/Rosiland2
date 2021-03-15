module LongestIncreasingSequence

open System.Text
open FParsec

let isIncreasing x y = x < y

let isDecreasing x y = x > y

let addToResultSeqByCriteria criteria acc next =
    if criteria (acc |> List.head) next then
        next::acc
    else
        acc

let findSeqByCriteria criteria input =
    List.fold (addToResultSeqByCriteria criteria) [input |> List.head] (input |> List.tail)
    |> List.rev

let getLongestSeq currentSeq nextSeq = 
    if (nextSeq |> List.length) > (currentSeq |> List.length) then
        nextSeq
    else
        currentSeq

let findLongestSeqByCriteriaForFirstElement criteria input =
    let head = input |> List.head
    let rec loop currentLongestSeq restOfInputSeq =
        match restOfInputSeq with
        | [] -> currentLongestSeq
        | _::xs ->
            match xs with
            | [] -> loop currentLongestSeq []
            | _ -> loop (getLongestSeq currentLongestSeq (findSeqByCriteria criteria (head::xs))) xs
    loop [] input

let findLongestByCriteria criteria input =
    let rec loop currentLongestSeq values =
        match values with
        | [] -> currentLongestSeq
        | _::xs -> loop (getLongestSeq currentLongestSeq (findLongestSeqByCriteriaForFirstElement criteria values)) xs
    loop [] input

type LongSubSampleData = {count:int; sequence:int list}

let longSubSampleParser = 
    let countParser = pint32 .>> newline
    let seqParser = many (spaces >>? pint32)
    tuple2 countParser seqParser   

let parseSingleEntry input =
    let reply = run longSubSampleParser input
    match reply with
    | Success(result,_,_) -> result |> (fun r -> {count = fst(r); sequence = snd(r)})
    | Failure _ -> {count = 0; sequence = []}
    
let parseSampleEntriesFile path =
    let entries = many1 longSubSampleParser
    let reply = runParserOnFile entries () path Encoding.UTF8
    match reply with
    | Success(result,_,_) -> result |> List.map (fun r -> {count = fst(r); sequence = snd(r)})
    | Failure _ -> List.empty
    
     