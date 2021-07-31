module Converter 
    
open System.IO
open Xunit.Abstractions

type Converter( output : ITestOutputHelper ) =
    inherit TextWriter()
    override __.Encoding = stdout.Encoding
    override __.WriteLine message = output.WriteLine message
    override __.Write message = output.WriteLine message
