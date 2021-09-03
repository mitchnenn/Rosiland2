module ListUtilitiesTests

open System.IO
open Xunit
open Xunit.Abstractions
open FsUnit
open MathUtilities
open ListUtilities
open ParseFasta

type ``List utilities tests`` (output:ITestOutputHelper) =

    [<Fact>]
    member this.``Verify permuations test`` () =
        // Arrange
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "shortest-superstring-2.txt")
        let fastaSeqs = parseFastaEntries path |> List.map (fun e -> e.Sequence)

        // Act
        let permutations = permute fastaSeqs

        // Assert
        output.WriteLine(sprintf "%A" permutations)
        permutations |> List.length |> should equal (factorial 4)

