module ShortestSuperstring

open ListUtilities

let overlap (first:string) (second:string) =
    let requiredOverlap = first.Length / 2
    let rec loop index = 
        if index >= requiredOverlap then
            None
        else if second.StartsWith(first.Substring(index)) then
            Some (first.Substring(0, index) + second )
        else
            loop (index+1)
    loop 0

let overlapReadsWithFirst (reads:string list) : string list =
    let rec loop first left acc =
        match left with
        | [] -> acc |> List.rev
        | x::xs ->
            let newAcc = match (overlap first x) with
                         | Some r -> r::acc
                         | None -> acc
            loop first xs newAcc
    loop reads.Head reads.Tail []

let permutateReadsAndFindOverlaps (reads:string list) : string list =
    let rec loop (left:string list list) (acc:string list) =
        match left with
        | [] -> acc |> List.distinct
        | x::xs ->
            let newAcc = match (overlapReadsWithFirst x) with
                         | [] -> acc
                         | ovs -> acc@ovs
            loop xs newAcc
    loop (permute reads) []

let findShortestSuperString (reads:string list) : string =
    let rec loop left =
       let overlaps = permutateReadsAndFindOverlaps left
       match overlaps.Length with
       | 0 -> ""
       | 1 -> overlaps.[0]
       | _ -> loop overlaps
    loop reads