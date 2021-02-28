module LongestIncreasingSequenceTests

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
        let result = isIncrease x y
        output.WriteLine(sprintf "%b" result)
        // Assert
        result |> should equal expected

    [<Theory>]
    [<InlineData(1,2,false)>]
    [<InlineData(4,3,true)>]
    member this.``Decrease test`` (x, y, expected) =
        // Arrange
        // Act
        let result = isDecrease x y
        output.WriteLine(sprintf "%b" result)
        // Assert
        result |> should equal expected

type ``Find increasing sequence test`` (output:ITestOutputHelper) =
    
    [<Fact>]
    member this.``Find first increasing sequence test`` () =
        // Arrange.
        let input = [8; 2; 1; 6; 5; 7; 4; 3; 9]
        // Act.
        let result = findSub isIncrease input
        output.WriteLine(sprintf "%A" result)
        // Assert.
        result |> should equal [8; 9]

    [<Fact>]
    member this.``Find all increasing sequences`` () =
        // Arrange.
        let input = [8; 2; 1; 6; 5; 7; 4; 3; 9]
        // Act.
        let result = findAllSubs isIncrease input
        output.WriteLine(sprintf "%A" result)
        // Assert.
        result |> should equal [2; 6; 7; 9]

    [<Fact>]
    member this.``Find first decreasing sequence`` () =
        // Arrange.
        let input = [8; 2; 1; 6; 5; 7; 4; 3; 9]
        // Act.
        let result = findAllSubs isDecrease input
        output.WriteLine(sprintf "%A" result)
        // Assert.
        result |> should equal [8; 6; 5; 4; 3]
        