module FParsecTestUtils

open FParsec

type UserState = unit // doesn't have to be unit, of course

type Parser<'t> = Parser<'t, UserState>

let test p str =
    match run p str with
    | Success(result, _, _)   -> sprintf "Success: %A" result
    | Failure(errorMsg, _, _) -> sprintf "Failure: %s" errorMsg
