module StringUtilities

open System
open System.Text

let charCount x = Seq.filter ((=) x) >> Seq.length        

let strCompareIgnoreCase a1 a2 =
    0 = String.Compare(a1, a2, StringComparison.InvariantCultureIgnoreCase)

let chunkString size str  =
     let rec loop (s:string) accum =
         let branch = s.Length > size
         match branch with
         | true -> loop (s.[size..]) (s.[0..size-1]::accum)
         | false -> s::accum
     (loop str []) |> List.rev

let toUpper (x:string) = x.ToUpper()

let explode (x:string) = x.ToCharArray()

let implode (x:char list) =
    let sb = StringBuilder(x.Length)
    x |> List.iter (sb.Append >> ignore)
    sb.ToString()

let filter (where:string list) (except:string list) : string list =
    where
    |> List.filter (fun i -> except |> List.exists (fun t -> t = i) |> not)

