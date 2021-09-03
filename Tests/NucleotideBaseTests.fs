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

    [<Fact>]
    member __.``Transscribe DNA to RNA test`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "transscirbe_dna_to_rna.txt")
        let dnaString = File.ReadAllText(path).Trim()
        output.WriteLine(sprintf "%s" dnaString)
        // Act.
        let rnaString = transscribeDnaStringToRnaString dnaString
        // Assert.
        rnaString |> should equal "GAUGGAACUUGACUACGUAAAUU"
        output.WriteLine(rnaString)


    [<Theory>]
    [<InlineData("ACGT", "ACGT")>]
    [<InlineData("AAAACCCGGT", "ACCGGGTTTT")>]
    member __.``Reverse compliment DNA string test`` (dnaString, expected) =
        // Arrange.
        // Act.
        let result = reverseComplimentNucleotideBase dnaString
        // Assert.
        result |> should equal expected

    [<Fact>]
    member __.``Reverse compliment problem solved test`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "reverse_compliment_input.txt")
        let dnaString = File.ReadAllText(path).Trim()
        // Act.
        let result = reverseComplimentNucleotideBase dnaString
        output.WriteLine(sprintf "%s" result)
        // Assert.
        result |> should equal ("TCGAGACGAAGTAATACTCTTCAACGATACTTCATGGC"
                                + "CTACAACTTTGGCTGAGCATATCAAATATGTCGCTTG"
                                + "ACGAGTTTCAACTATCAATCCTTTATGTTATCGCTCCC"
                                + "AGACAGACAACGCAAGTTATCTTACTATTTCGGAAAAA"
                                + "AGGGCTGAACTGGTCGATACGCAGTAAGCAATCGCCGAA"
                                + "GCCCGTACCCATTGTGGCTAGCCCACCTAAATGCCTGTG"
                                + "GTGCCCGCACGTAGTTCTAACTTGTGAAAATGGGGCTT"
                                + "CTTTGTTGTTAGAGACGCTGTACCCGCCGCCCAATGTT"
                                + "GAAGGTACTAGTAGCAGGCGCTGTCCTCGAAAGTTCCCC"
                                + "CGTTTCCATTATGCTGACACGAGCAGGTATTGGGATAGC"
                                + "TAGGCTGCGGAGTATGATTTGGGAGCGCCCTCATGCCCA"
                                + "TTCCTAGAATCCAAAATAACACTGTGAGATGGGCAAAGT"
                                + "ACACAATGGGTTTGGACGAGGATACCCATCGCCCCTCCA"
                                + "TGTATCCCCTTAGTGACCCCTGGGAAAAACCAGTGACCT"
                                + "GGTGGTATCGGGCATGCTCAACACAAGTTGTCTTGACACG"
                                + "CCTGACCAGTGCATGGGGAGCCACATACCATGTCAATT"
                                + "TAGCTATTTAAATGACTCAATTGGAAATTCATGAACGA"
                                + "GCCACTACCACGCCGAGTCGTGCCTGGGATACACACGGC"
                                + "TAAGCCGGCTAACAAAATTAGCACCGAATGTGATCCACG"
                                + "CAGCGGGTTCGTTCCTAGGCAAACGTCAGAAAGTAGGA"
                                + "AGGATTATAGCTTGGTGTTGATATTCCCTGTCTACATGCT"
                                + "GCGGGTGACGCTCCTGTCTAAT")

