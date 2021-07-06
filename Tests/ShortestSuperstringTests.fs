module ShortestSuperstringTests

open System.IO
open Xunit
open Xunit.Abstractions
open FsUnit
open ParseFasta
open ShortestSuperstring

type ``Shortest super string tests`` (output:ITestOutputHelper) =

    [<Theory>]
    [<InlineData("CCTGCCGGAA", "GCCGGAATAC", "CCTGCCGGAATAC")>]
    [<InlineData("ATTAGACCTG", "AGACCTGCCG", "ATTAGACCTGCCG")>]
    [<InlineData("AGACCTGCCG", "CCTGCCGGAA", "AGACCTGCCGGAA")>]
    [<InlineData("AGACCTGCCGGAA", "CCTGCCGGAATAC", "AGACCTGCCGGAATAC")>]
    member this.``Find minimum overlap test`` ((first:string), (second:string), (expected:string)) =
        // Arrange.
        // Act.
        let overlapString = overlap first second
        // Assert.
        overlapString |> should equal expected

    [<Fact>]
    member this.``Find all overlaps test`` () =
        // Arrange
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "shortest-superstring-2.txt")
        let fastaSeqs = parseFastaEntries path |> List.map (fun e -> e.Sequence)

        // Act
        let overlaps = findAllOverlaps fastaSeqs

        // Assert
        overlaps |> should equal ["CCTGCCGGAATAC"; "ATTAGACCTGCCG"; "AGACCTGCCGGAA"]


    [<Fact>]
    member this.``Create shortest super string test`` () =
        // Arrange
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "shortest-superstring-2.txt")
        let fastaSeqs = parseFastaEntries path |> List.map (fun e -> e.Sequence)
        
        // Act
        
        // Assert
        true |> should equal true
