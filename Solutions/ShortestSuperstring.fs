module ShortestSuperstring

let readStartsWith (aRead:string) (beginning:string) : bool =
    aRead.StartsWith(beginning)

let joinOverlapped (overlap:string) (first:string) (second:string) : string =
    first + second.Substring(overlap.Length)

let getOverlap (first:string) (second:string) : string =
    let start = first.Length - second.Length + 1
    let minOverlap = start + second.Length / 2
    let possibleOverlaps = seq { for i = start to minOverlap do yield first.Substring(i) }
    let someOverlap = possibleOverlaps |> Seq.tryFind (fun p -> p |> readStartsWith second)
    match someOverlap with
    | Some(overlap) -> joinOverlapped overlap first second
    | None -> first

let findOverlaps (read:string) (otherReads:string list) : string =
    let rec loop rest superstring =
        match rest with
        | [] -> superstring
        | x::xs -> loop xs (getOverlap superstring x)
    loop otherReads read        

let findShortestSuperstring (reads:string list) : string =
    findOverlaps reads.Head reads.Tail
    