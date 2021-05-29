module LongestIncreasingSequence

open System.Text
open FParsec

type LongSubSampleData = {count:int; sequence:int list}

let longSeqSampleParser = 
    let countParser = pint32 .>> newline
    let seqParser = many (spaces >>? pint32)
    tuple2 countParser seqParser   

let parseSingleEntry input =
    let reply = run longSeqSampleParser input
    match reply with
    | Success(result,_,_) -> result |> (fun r -> {count = fst(r); sequence = snd(r)})
    | Failure _ -> {count = 0; sequence = []}
    
let parseSampleEntriesFile path =
    let entries = many1 longSeqSampleParser
    let reply = runParserOnFile entries () path Encoding.UTF8
    match reply with
    | Success(result,_,_) -> result |> List.map (fun r -> {count = fst(r); sequence = snd(r)})
    | Failure _ -> List.empty
    
let isIncreasing (x:int) (y:int) = y > x
let isDecreasing (x:int) (y:int) = x > y

let binarySearchLargestValue
    (compareFunc:int->int->bool)
    (currentInputIndex:int)
    (_L:int)
    (input:int list)
    (_M:int array) =
    let mutable lo = 1;
    let mutable hi = _L;
    while lo <= hi do
        let mid = int (System.Math.Ceiling (float(lo + hi) / 2.0)) 
        if compareFunc input.[_M.[mid]] input.[currentInputIndex] then
            lo <- mid + 1;
        else
            hi <- mid - 1;
    lo

let reconstructLongestSeq
    (_L:int)
    (_M:int array)
    (input:int list)
    (_P: int array) = 
    let S : int array = Array.zeroCreate _L;
    let mutable k : int = _M.[_L];
    for index = (_L - 1) downto 0 do
        S.[index] <- input.[k];
        k <- _P.[k];
    S

let findLongestSeq (input:int list) compareFunc =
    let _P : int array = Array.zeroCreate input.Length
    let _M : int array = Array.zeroCreate input.Length
    let mutable _L = 0
    for currentInputIndex = 0 to input.Length - 1 do
        let newL = binarySearchLargestValue compareFunc currentInputIndex _L input _M
        _P.[currentInputIndex] <- _M.[newL - 1];
        _M.[newL] <- currentInputIndex
        if newL > _L then
            _L <- newL
    reconstructLongestSeq _L _M input _P
    |> Array.toList
