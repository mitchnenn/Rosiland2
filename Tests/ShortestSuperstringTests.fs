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

    [<Theory>]
    [<InlineData("CCTGCCGGAA", "GCCGGAATAC", "CCTGCCGGAATAC")>]
    [<InlineData("ATTAGACCTG", "AGACCTGCCG", "ATTAGACCTGCCG")>]
    [<InlineData("AGACCTGCCG", "CCTGCCGGAA", "AGACCTGCCGGAA")>]
    [<InlineData("AGACCTGCCGGAA", "CCTGCCGGAATAC", "AGACCTGCCGGAATAC")>]
    member this.``Find minimum overlap test`` ((prefix:string), (suffix:string), (expected:string)) =
        // Arrange.
        // Act.
        let overlapStringOption = overlap prefix suffix
        // Assert.
        match overlapStringOption with
        | Some r -> r |> should equal expected
        | None -> ()

    [<Fact>]
    member this.``Find overlaps for prefix with many suffixes test`` () =
        // Arrange.
        let reads = ["CCTGCCGGAA"; "ATTAGACCTG"; "AGACCTGCCG"; "GCCGGAATAC"]
        // Act.
        let result = overlapsForPrefixAndSuffixes reads.Head reads.Tail
        // Assert.
        result |> List.length |> should equal 1
        result.[0] |> should equal "CCTGCCGGAATAC"

    [<Fact>]
    member this.``Permute reads test.`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "shortest-superstring-2.txt")
        let fastaSeqs = parseFastaEntries path |> List.map (fun e -> e.Sequence)
        // Act.
        let permutations = permuteReads fastaSeqs
        output.WriteLine(sprintf "%A" permutations)
        // Assert.
        permutations |> List.length |> should equal fastaSeqs.Length
        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "permutations.txt"), sprintf "%A" permutations)

    [<Fact>]
    member __.``Find all overlaps for prefixes with permutations of suffixes test`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "shortest-superstring-2.txt")
        let fastaSeqs = parseFastaEntries path |> List.map (fun e -> e.Sequence)
        // Act.
        let overlaps = findOverlapsForPrefixesAndSuffixes fastaSeqs
        // Assert.
        ()

    [<Fact>]
    member this.``Find all overlaps for all permutations test`` () =
        // Arrange
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "shortest-superstring-2.txt")
        let fastaSeqs = parseFastaEntries path |> List.map (fun e -> e.Sequence)
        // Act
        let overlaps = permutateReadsAndFindOverlaps fastaSeqs
        // Assert
        overlaps |> should equivalent ["CCTGCCGGAATAC"; "ATTAGACCTGCCG"; "AGACCTGCCGGAA"]

    [<Fact>]
    member this.``Find shortest super string test`` () =
        // Arrange
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "shortest-superstring-2.txt")
        let fastaSeqs = parseFastaEntries path |> List.map (fun e -> e.Sequence)
        // Act
        let result = findShortestSuperString fastaSeqs
        output.WriteLine(sprintf "%s" result)
        // Assert
        result |> should equal "ATTAGACCTGCCGGAATAC"

    [<Fact>]
    member this.``Answer Rosalind problem`` () =
        // Arrange
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "shortest-superstring-1.txt")
        let fastaSeqs = parseFastaEntries path |> List.map (fun e -> e.Sequence)
        // Act
        let result = findShortestSuperString fastaSeqs
        output.WriteLine(sprintf "%s" result)
        // Assert
        let answerPath = Path.Combine(Directory.GetCurrentDirectory(), "shortest-superstring.txt")
        File.WriteAllText(answerPath, result)
