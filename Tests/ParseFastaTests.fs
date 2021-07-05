module ParseFastaTests

open System.Text
open System.IO
open ParseFasta
open FParsec
open Xunit
open Xunit.Abstractions
open FsUnit

type ``Parse Fasta file tests`` (output:ITestOutputHelper) =
    
    [<Fact>]
    member ths.``Read raw entries test`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "data.fasta")
        // Act.
        let entries = readRawEntries path |> Seq.toList
        // Assert
        entries |> List.iter (fun i -> output.WriteLine(i))
        entries |> List.length |> should equal 9

    [<Fact>]
    member this.``Parse file happy path`` () =
        // Arrange
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "data.fasta")
        // Act
        let fastaEntries = parseFastaEntries path
        // Assert
        fastaEntries |> List.iter (fun i -> output.WriteLine(sprintf "%A" i))
        fastaEntries.Length |> should equal 9
        fastaEntries.[0].Sequence |> should startWith "GCCGGGTGCCGTGGCAAGAGTAGC"
        fastaEntries.[0].Sequence |> should endWith "ATTTCGCCGTAGCCGCAGCGAA"
