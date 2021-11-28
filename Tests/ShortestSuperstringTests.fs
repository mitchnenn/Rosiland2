module ShortestSuperstringTests

open System
open System.IO
open Xunit
open Xunit.Abstractions
open FsUnit
open Converter
open ParseFasta
open ShortestSuperstring

type ``Shortest super string tests`` (output:ITestOutputHelper) =
    do new Converter(output) |> Console.SetOut

    [<Fact>]
    member __.``Valid suffixes test`` () =
        // Arrange.
        let input = "ATTAGACCTG"
        // Act.
        let suffixes = validSuffixes input
        output.WriteLine(sprintf "%A" suffixes)
        // Assert.
        suffixes |> should equal ["ATTAGACCTG";"TTAGACCTG";"TAGACCTG";"AGACCTG";"GACCTG";"ACCTG"]

    [<Theory>]
    [<InlineData("ATTAGACCTG", "AGACCTGCCG", "ATTAGACCTGCCG")>]
    [<InlineData("ATTAGACCTG", "ATTAGACCTG", "")>]
    [<InlineData("ATTAGACCTG", "CCTGCCGGAA", "")>]
    member __.``Try find overlap test`` (currentRead:string, currentCompare:string, expected:string ) =
        // Arrange.
        // Act.
        let overlapOption = tryGetOverlap currentRead currentCompare
        let overlap = match overlapOption with | Some r -> r | None -> ""
        // Assert
        overlap |> should equal expected

    [<Fact>]
    member __.``Get overlaps test`` () =
        // Arrange.
        let reads = ["ATTAGACCTG"; "CCTGCCGGAA"; "AGACCTGCCG"; "GCCGGAATAC"]
        // Act.
        let overlaps = reads |> getOverlaps
        output.WriteLine(sprintf "%A" overlaps)
        // Assert.
        overlaps |> should equal ["ATTAGACCTGCCG"; "CCTGCCGGAATAC"; "AGACCTGCCGGAA"]

    [<Fact>]
    member __.``Concat overlaps test`` () =
        // Arrange.
        let overlaps = ["ATTAGACCTGCCG"; "CCTGCCGGAATAC"; "AGACCTGCCGGAA"]
        // Act.
        let superstring = concatOverlaps overlaps
        output.WriteLine(sprintf "%s" superstring)
        // Assert.
        superstring |> should equal "ATTAGACCTGCCGGAATAC"

    [<Fact>]
    member _.``Rosiland example shortest superstring test`` () =
        // Arrange
        let reads = ["ATTAGACCTG"; "CCTGCCGGAA"; "AGACCTGCCG"; "GCCGGAATAC"]
        // Act
        let result = getShortestSuperstring reads
        output.WriteLine(result)
        // Assert
        result |> should equal "ATTAGACCTGCCGGAATAC"

    [<Theory>]
    [<InlineData("shortest-superstring-2.txt")>]
    [<InlineData("shortest-superstring-1.txt")>]
    member _.``Fasta input shortest superstring test`` (filename:string) =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", filename)
        let reads = parseFastaEntries path |> List.map(fun r -> r.Sequence)
        // Act.
        let shortestSubstring = getShortestSuperstring reads
        printf "%s" shortestSubstring
        ()