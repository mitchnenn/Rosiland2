module ParseFasta

open System.IO
open System.Text
open FParsec

type FastaRecord = {Description:string; Sequence:string}

let descriptionStart = '>'

let isDescriptionStart (c:char) = c = descriptionStart

let allCharsExceptDescriptionStart (c:char) = (isDescriptionStart c) <> true

let readRawEntries (path:string) = seq {
    use stream = new FileStream (path, FileMode.Open)
    use charStream = new CharStream (stream, Encoding.UTF8)
    while charStream.IsEndOfStream <> true do
        let entry = charStream.ReadCharsOrNewlinesWhile(isDescriptionStart, allCharsExceptDescriptionStart, true)
        yield entry.Trim()
}

let id = pchar descriptionStart >>. many1CharsTill anyChar newline

let sequence = many1Chars ( anyChar .>> optional newline )

let parseEntry (entry:string) : FastaRecord =
    let recordParser = tuple2 id sequence
    let reply = run recordParser entry
    match reply with
    | Success(result,_,_) -> {Description=fst result; Sequence=snd result}
    | Failure(msg,_,_) -> failwith msg

let parseFastaEntries (path:string) =
    path
    |> readRawEntries
    |> Seq.toList
    |> List.map (fun e -> parseEntry e)
