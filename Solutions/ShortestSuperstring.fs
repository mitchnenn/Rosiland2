module ShortestSuperstring

let validSuffixes (read:string) : string seq =
    seq {
        for i in [0..read.Length / 2] do
            yield read.Substring(i)
    }

let tryGetOverlap (firstRead:string) (secondRead:string) : string option =
    validSuffixes firstRead
    |> Seq.tryFind(secondRead.StartsWith)
    |> Option.map(fun o -> firstRead + secondRead.Substring(o.Length))
    |> Option.bind(fun o -> if o = firstRead then None else Some(o) )

let getOverlaps (reads:string list) : string list =
    let rec loop (left:string list) (acc:string list) =
        match left with
        | [] -> acc
        | x::xs ->
            let overlaps = reads
                           |> List.map(tryGetOverlap x)
                           |> List.choose id
            loop xs (acc @ overlaps)
    loop reads []

let concatOverlaps (overlaps:string list) : string =
    match overlaps with
    | [] -> ""
    | _ ->
        let rec loop (left:string list) (acc:string) =
            match left with
            | [] -> acc
            | x::xs ->
                let nextAcc = match (tryGetOverlap acc x) with
                              | None -> acc
                              | Some o -> o
                loop xs nextAcc
        loop overlaps ""

let getShortestSuperstring (reads:string list) : string =
    reads
    |> getOverlaps
    |> concatOverlaps
