module LongestIncreasingSequenceTests

open System
open System.Diagnostics
open System.IO
open Xunit
open Xunit.Abstractions
open FsUnit
open LongestIncreasingSequence

type ``Increase and decrease function tests`` (output:ITestOutputHelper) =

    [<Theory>]
    [<InlineData(1,2,true)>]
    [<InlineData(4,3,false)>]
    member this.``Increase test`` (x, y, expected) =
        // Arrange
        // Act
        let result = isIncreasing x y
        output.WriteLine(sprintf "%b" result)
        // Assert
        result |> should equal expected

    [<Theory>]
    [<InlineData(1,2,false)>]
    [<InlineData(4,3,true)>]
    member this.``Decrease test`` (x, y, expected) =
        // Arrange
        // Act
        let result = isDecreasing x y
        output.WriteLine(sprintf "%b" result)
        // Assert
        result |> should equal expected

type ``Parse file tests`` (output:ITestOutputHelper) =
    [<Fact>]
    member this.``Parse single entry test``() =
        // Arrange.
        let singleEntry = $"5{Environment.NewLine}5 1 4 2 3"
        // Act.
        let result = parseSingleEntry singleEntry
        output.WriteLine(sprintf "%A" result)
        // Assert.
        result.count |> should equal 5
        result.sequence |> should equal [5; 1; 4; 2; 3]

    [<Fact>]
    member this.``Parse single sample entry in file test`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "LongestIncSeq1.txt")
        // Act.
        let result = parseSampleEntriesFile path
        output.WriteLine(sprintf "%A" result)
        // Assert.
        result |> List.length |> should equal 1

type ``Find increasing sequence test`` (output:ITestOutputHelper) =
    
    [<Fact>]
    member this.``Find first increasing sequence test`` () =
        // Arrange.
        let input = [8; 2; 1; 6; 5; 7; 4; 3; 9]
        // Act.
        let stopWatch = Stopwatch()
        stopWatch.Start()
        let result = findLongestSeq input isIncreasing
        stopWatch.Stop()
        output.WriteLine(sprintf "%A" result)
        output.WriteLine(sprintf "%d" stopWatch.ElapsedMilliseconds)
        // Assert.
        result |> should equal [1; 5; 7; 9]

    [<Fact>]
    member this.``Find first decreasing sequence`` () =
        // Arrange.
        let input = [8; 2; 1; 6; 5; 7; 4; 3; 9]
        // Act.
        let result = findLongestSeq input isDecreasing
        output.WriteLine(sprintf "%A" result)
        // Assert.
        result |> should equal [8; 6; 5; 4; 3;]

    [<Fact>]
    member this.``Detect longest increasing and decreasing sequence`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "LongestIncSeq1.txt")
        let input = parseSampleEntriesFile path |> List.head
        output.WriteLine(sprintf "%A" input)
        // Act.
        let inc = findLongestSeq input.sequence isIncreasing
        output.WriteLine(sprintf "%A" inc)
        let dec = findLongestSeq input.sequence isDecreasing
        output.WriteLine(sprintf "%A" dec)
        // Assert
        inc |> should equal [1;2;3]
        dec |> should equal [5;4;3]
        
    member this.printOutput (alist:int list) =
        let templist = alist |> List.map (fun i -> string (i.ToString() + " ")) |> List.toArray
        let astring = String.Concat(templist)
        output.WriteLine(astring)
    
    [<Fact>]
    member this.``Solve the Rosiland problem`` () =
        // Arrange.
        let path = Path.Combine(__SOURCE_DIRECTORY__, "Data", "LongestIncSeq2.txt")
        let input = parseSampleEntriesFile path |> List.head
        // Act.
        output.WriteLine("Increasing sequence ...")
        let inc = findLongestSeq input.sequence isIncreasing
        output.WriteLine("printing ...")
        this.printOutput inc
        output.WriteLine("Decreasing sequence ...")
        let dec = findLongestSeq input.sequence isDecreasing
        output.WriteLine("printing ...")
        this.printOutput dec
        true |> should equal true
