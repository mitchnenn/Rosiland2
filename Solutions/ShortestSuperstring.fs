module ShortestSuperstring

open ListUtilities

let overlap (prefix:string) (suffix:string) =
    let requiredOverlap = prefix.Length / 2
    let rec loop index = 
        if index >= requiredOverlap then
            None
        else if suffix.StartsWith(prefix.Substring(index)) then
            Some (prefix.Substring(0, index) + suffix)
        else
            loop (index+1)
    loop 0

let overlapsForPrefixAndSuffixes (prefix:string) (suffixes:string list) : string list =
    let rec loop left acc =
        match left with
        | [] -> acc |> List.rev
        | x::xs ->
            let overlaps = match (overlap prefix x) with
                           | Some r -> r::acc
                           | None -> acc
            loop xs overlaps
    loop suffixes []

let permuteReads (reads:string list) : string list list = 
    let acc : string list array = Array.zeroCreate reads.Length
    for i = 0 to reads.Length - 1 do
        let current : string array = Array.zeroCreate reads.Length
        current.[0] <- reads.[i]
        let mutable k = 1
        for j = 0 to reads.Length - 1 do
            if j <> i then
                current.[k] <- reads.[j]
                k <- k + 1
        acc.[i] <- (current |> Array.toList)
    acc |> Array.toList

let findOverlapsForPrefixesAndSuffixes (reads:string list) : string list =
    let rec loop (permutations:string list list) (allOverlaps:string list) =
        match permutations with
        | [_] | [] -> allOverlaps |> List.distinct
        | x::xs -> loop xs (allOverlaps @ (overlapsForPrefixAndSuffixes x.Head x.Tail))
    loop (permute reads) []

let permutateReadsAndFindOverlaps (reads:string list) : string list =
    let rec loop (left:string list list) (acc:string list) =
        match left with
        | [] -> acc |> List.distinct
        | x::xs ->
            let newAcc = match (overlapsForPrefixAndSuffixes x.Head x.Tail) with
                         | [] -> acc
                         | ovs -> acc@ovs
            loop xs newAcc
    loop (permuteReads reads) []

let findShortestSuperString (reads:string list) : string =
    let result = reads
                 |> permutateReadsAndFindOverlaps
                 |> permutateReadsAndFindOverlaps
                 |> permutateReadsAndFindOverlaps
                 |> List.sortByDescending (fun i -> i.Length)
                 
    result |> List.head
