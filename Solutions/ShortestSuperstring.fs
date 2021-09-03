module ShortestSuperstring

open ListUtilities

let validSuffixes (read:string) : string seq =
    seq {for i in [0..read.Length / 2] do yield read.Substring(i)}

let tryGetOverlap (firstRead:string) (secondRead:string) : string option =
    validSuffixes firstRead
    |> Seq.tryFind(secondRead.StartsWith)
    |> Option.map(fun o -> firstRead + secondRead.Substring(o.Length))
    |> Option.bind(fun o -> if o = firstRead then None else Some(o) )

let getOverlaps (reads:string list) : string list =
    reads.Tail
    |> List.map(fun secondRead -> tryGetOverlap reads.Head secondRead)
    |> List.choose id

let getShortestSuperstring (reads:string list) : string =
    permute reads
    |> List.map(fun l -> getOverlaps l)
    |> List.filter(fun l -> l.Length > 0)
    |> List.concat
    |> List.distinct
    |> getOverlaps
    |> List.sortByDescending(fun l -> l.Length)
    |> List.head
