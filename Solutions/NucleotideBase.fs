module NucleotideBases

type NucleotideBase =
    | Adenine
    | Cytosine
    | Guanine
    | Thymine
    | Uracil

let charToNucleotideBase (c:char) =
    match c with
    | 'A' | 'a' -> Adenine
    | 'C' | 'c' -> Cytosine
    | 'G' | 'g' -> Guanine
    | 'T' | 't' -> Thymine
    | 'U' | 'u' -> Uracil
    | _ -> failwith "Not a Nucleotide Base."

let nucleotideBaseToChar (n:NucleotideBase) =
    match n with
    | Adenine -> 'A'
    | Cytosine -> 'C'
    | Guanine -> 'G'
    | Thymine -> 'T'
    | Uracil -> 'U'

let getNucleotides (dnaString:string) =
     dnaString.Trim().ToUpper().ToCharArray()
     |> Array.toList
     |> List.map(fun n -> charToNucleotideBase n)

let countNucleotideBases (dnaString:NucleotideBase list) =
    dnaString
    |> List.sort
    |> List.countBy id

let dnaBaseToRnaBase (n:NucleotideBase) =
    match n with
    | Thymine -> Uracil
    | _ -> n

let transscribeDnaStringToRnaString (dnaString:string) : string =
    let listOfChars = dnaString
                      |> getNucleotides
                      |> List.map(fun n -> dnaBaseToRnaBase n)
                      |> List.map(fun n -> nucleotideBaseToChar n)
                      |> List.toArray
    new string(listOfChars)
