module ListUtilities

let permute list =
  let rec inserts e = function
    | [] -> [[e]]
    | x::xs as list -> (e::list)::[for xs' in inserts e xs -> x::xs']

  List.fold (fun accum x -> List.collect (inserts x) accum) [[]] list

