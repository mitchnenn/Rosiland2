module FParsecHelper

open FParsec
open Xunit.Abstractions

type UserState = unit // doesn't have to be unit
type Parser<'t> = Parser<'t, UserState>

let test (output:ITestOutputHelper) p str =
    match run p str with
    | Success(result,_,_) -> output.WriteLine(sprintf "%A" result)
    | Failure(errormsg,_,_) -> output.WriteLine(sprintf "%s" errormsg)
