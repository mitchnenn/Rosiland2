module ParseFasta

open System.Text
open FParsec

let delimiter = ">"

let id = pstring delimiter
         >>. many1CharsTill anyChar newline
         
let sequenceChars = 
    let isSeqChar c = c = 'A' || c = 'C' || c = 'G' || c = 'T'
    many1Satisfy isSeqChar

let sequence =
    many1Strings (sequenceChars .>> newline)

let entries = many1 (tuple2 id sequence)

type FastaRecord = {Id:string; Sequence:string}

let parseFastaEntries (path:string) =
    let reply = runParserOnFile entries () path Encoding.UTF8
    match reply with
    | Success(result, _, _) -> result |> List.map(fun t -> {Id=(fst t);Sequence=(snd t)})
    | Failure(_,_,_) -> List.empty
