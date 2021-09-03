module ShortestSuperstring

open ListUtilities
open StringUtilities

let validSuffixes (read:string) : string seq =
    seq {for i in [0..read.Length / 2] do yield read.Substring(i)}

let tryGetOverlap (firstRead:string) (secondRead:string) : string option =
    validSuffixes firstRead
    |> Seq.tryFind(secondRead.StartsWith)
    |> Option.map(fun o -> firstRead + secondRead.Substring(o.Length))
    |> Option.bind(fun o -> if o = firstRead then None else Some(o) )

let getOverlaps (reads:string list) : string list =
    match reads with
    | [] -> []
    | _ ->
        reads.Tail
        |> List.map(fun secondRead -> tryGetOverlap reads.Head secondRead)
        |> List.choose id

let permuteForward (reads:string list) : string list seq =
    seq {
        for read in reads do
            yield [read] @ (filter reads [read])
    }

let permuteForwardAndGetOverlaps (reads:string list) : string list =
    permuteForward reads
    |> Seq.toList
    |> List.map(fun l -> getOverlaps l)
    |> List.filter(fun l -> l.Length > 0)
    |> List.concat
    |> List.distinct

let getShortestSuperstring (reads:string list) : string =
    let superStrings = reads
                       |> permuteForwardAndGetOverlaps
                       |> permuteForwardAndGetOverlaps
                       |> List.sortByDescending(fun l -> l.Length)
    match superStrings with
    | [] -> ""
    | _ ->
        superStrings.Head
