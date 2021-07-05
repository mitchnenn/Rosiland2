module ShortestSuperstringTests

open System.IO
open Xunit
open Xunit.Abstractions
open FsUnit
open ParseFasta
open ShortestSuperstring

type ``Shortest super string tests`` (output:ITestOutputHelper) =

    [<Fact>]
    member this.``Create shortest super string test`` () =
        // Arrange
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "shortest-superstring-2.txt")
        let reads = parseFastaEntries path |> List.map (fun f -> f.Sequence) 
        
        // Act
        // there exists a unique way to reconstruct the entire chromosome from these reads
        // by gluing together pairs of reads that overlap
        // by more than half their length.
        let result = findShortestSuperstring reads 
        output.WriteLine(result)
        
        // Assert
        result |> should equal "ATTAGACCTGCCGGAATAC"
