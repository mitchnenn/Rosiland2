module NucleotideBases

type NucleotideBase =
    | Adenine
    | Cytosine
    | Guanine
    | Thymine

let charToNucleotideBase (c:char) =
    match c with
    | 'A' | 'a' -> Adenine
    | 'C' | 'c' -> Cytosine
    | 'G' | 'g' -> Guanine
    | 'T' | 't' -> Thymine
    | _ -> failwith "Not a Nucleotide Base."

let getNucleotides (dnaString:string) =
     dnaString.Trim().ToUpper().ToCharArray()
     |> Array.toList
     |> List.map(fun n -> charToNucleotideBase n)

let countNucleotideBases (dnaString:NucleotideBase list) =
    dnaString
    |> List.sort
    |> List.countBy id
