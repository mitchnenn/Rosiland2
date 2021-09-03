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
    | _ -> failwith (sprintf "Undefined Nucleotide Base: '%c'." c)

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
    new string(dnaString
               |> getNucleotides
               |> List.map(fun n -> dnaBaseToRnaBase n)
               |> List.map(fun n -> nucleotideBaseToChar n)
               |> List.toArray)

let complimentNucleotideBase (n:NucleotideBase) =
    match n with
    | Adenine -> Thymine
    | Thymine -> Adenine
    | Cytosine -> Guanine
    | Guanine -> Cytosine
    | _ -> failwith (sprintf "Nucleotide base compliment not defined: '%A'." n)

let reverseComplimentNucleotideBase (dnaString:string) : string =
    new string(
        dnaString
        |> getNucleotides
        |> List.rev
        |> List.map (fun n -> complimentNucleotideBase n)
        |> List.map (fun n -> nucleotideBaseToChar n)
        |> List.toArray)

