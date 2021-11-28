module ParseFastaTests

open System.Text
open System.IO
open ParseFasta
open Xunit
open Xunit.Abstractions
open FsUnit

type ``Parse Fasta file tests`` (output:ITestOutputHelper) =
    
    [<Fact>]
    member this.``Read raw entries test`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "data.fasta")
        // Act.
        let entries = readRawEntries path |> Seq.toList
        // Assert
        entries |> List.iter (fun i -> output.WriteLine(i))
        entries |> List.length |> should equal 9

    [<Fact>]
    member this.``Parse Fasta entry test.`` () =
        // Arrange.
        let entry = new StringBuilder()
        entry.AppendLine(">Rosalind_7067") |> ignore
        entry.AppendLine("GCCGGGTGCCGTGGCAAGAGTAGCGGTTCTCAGACCATAGTCGCTTCCATCAAGAGGGGG") |> ignore
        entry.AppendLine("TCACGGCGGACGACTGCTTCGGTATAGCAGGCGGTCATTATTCGTTCGTATAATGGATAT") |> ignore
        entry.AppendLine("CTGTCAGTCTTCTAGACGAGTTACAACCGTAGAGGTTGTTCTTAGGTGGATCGCAGCGTA") |> ignore
        entry.AppendLine("CCGCACTTGCCGCTTTGGACAAGTGCTACTGTTCAACACTCCTACTCGTGTGTCTGGGAT") |> ignore
        entry.AppendLine("CCCGAATAAATACCACAGAGGTATCCCCCTCCGATTTGATCATGGAACCACCCTCCGCCA") |> ignore
        entry.AppendLine("GATCTCCCAGAGAGCGGGGTAGTCGGGTTTTATATGTTATACAGTCTATGGAACAATTCT") |> ignore
        entry.AppendLine("TTTCACCCCATCAATAATCGCACCCATAAATGGTTATTGGTTCTCGTACTCCCTACATAA") |> ignore
        entry.AppendLine("CCTGCAAGTCGCTCCCATTGTTACACCTGCGTAACAGAATTCCATAGTGTAGGTTTAGCT") |> ignore
        entry.AppendLine("GCTGTGGGCCCTCTAGCCTTGTCCTTAGGTCTCACTGCTAAAGCACCGAAATTAACGTAA") |> ignore
        entry.AppendLine("GGCTTTAAGTAAGAGTTCCTTGCAGAGAATTTAAGAAGCGAACCCGCCGCTTTTCATCCA") |> ignore
        entry.AppendLine("CGTTGAGGCCAGCTCCACTCTTTTGAACAGACAGTCGCGAAGGACACCAGGGGTAACCTA") |> ignore
        entry.AppendLine("TTAGACCTCAACCGGTCATAGCTTGGACCAGAGACGGCGGACGCGGAACCTCAGTGATAG") |> ignore
        entry.AppendLine("GATGCACTGAACCAAATCGCCGTCCTCGCATCGCAAGACAAAGCCCAGTGTTACTGTACG") |> ignore
        entry.AppendLine("CACAGATAGCATACGATCTCCTTCCGCCAGGAGGGGAGTGATAAGACCTACAGTGGTTCT") |> ignore
        entry.AppendLine("AAGTACCGCACGATGCGGGGAGGCGACCCATGATGTCAGAAGCGATACATGCACCGTCCC") |> ignore
        entry.AppendLine("CAGACACGTATAGCTGCGATTCTCTATAAGTGGCCCCCAGCCTTCAACAGTTAGCAATCA") |> ignore
        entry.AppendLine("TTTCGCCGTAGCCGCAGCGAA") |> ignore
        // Act.
        let fasta = parseEntry (entry.ToString())
        // Assert.
        fasta.Description |> should equal "Rosalind_7067"
        fasta.Sequence |> should startWith "GCCGGGTGCCGTGGCAAGAGTAGCGGTTCTCAGACCATAGTCGCTTCCATCAAGAGGGGGTC"
        fasta.Sequence |> should endWith "ATCATTTCGCCGTAGCCGCAGCGAA"

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
