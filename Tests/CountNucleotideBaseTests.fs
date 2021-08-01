module CountNucleotideBaseTests

open System
open System.IO
open Converter
open Xunit
open Xunit.Abstractions
open FsUnit
open NucleotideBases

type CountNucleotideBaseTests(output:ITestOutputHelper) =
    do new Converter(output) |> Console.SetOut

    [<Fact>]
    member __.``Count DNA Nucleotide Base Test`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "dna_nucleotide_bases.txt")
        let dnaString = File.ReadAllText(path).Trim()
        output.WriteLine(sprintf "%s" dnaString)
        // Act.
        let counts = dnaString
                     |> getNucleotides
                     |> countNucleotideBases
        output.WriteLine(sprintf "%A" counts)
        // Assert
        counts |> List.length |> should equal 4
        counts |> List.map(fun (_,c) -> c) |> should equal [20;12;17;21]
