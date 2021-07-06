module ShortestSuperstring

let overlap (first:string) (second:string) : string =
    let requiredOverlap = first.Length / 2
    let rec loop index = 
        if index >= requiredOverlap then
            ""
        else if second.StartsWith(first.Substring(index)) then
            first.Substring(0, index) + second
        else
            loop (index+1)
    loop 0

let findAllOverlaps (reads:string list) : string list =

    list.Empty
