module ParseFastaTests

open System.IO
open ParseFasta
open Xunit
open Xunit.Abstractions
open FsUnit

type ``Parse Fasta file tests`` (output:ITestOutputHelper) =
    
    [<Fact>]
    member this.``Parse file happy path`` () =
        // Arrange
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "data.fasta")
        // Act
        let fastaEntries = parseFastaEntries path
        // Assert
        fastaEntries.Length |> should equal 9
        fastaEntries.[0].Sequence |> should startWith "GCCGGGTGCCGTGGCAAGAGTAGC"
        fastaEntries.[0].Sequence |> should endWith "ATTTCGCCGTAGCCGCAGCGAA"