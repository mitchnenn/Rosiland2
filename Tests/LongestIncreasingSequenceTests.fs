module LongestIncreasingSequenceTests

open Xunit
open Xunit.Abstractions
open FsUnit
open Problems.LongestIncreasingSequence

type ``Increase and decrease function tests`` (output:ITestOutputHelper) =

    [<Theory>]
    [<InlineData(1,2,true)>]
    [<InlineData(4,3,false)>]
    member this.``Increase test`` (x, y, expected) =
        // Arrange
        // Act
        let result = increase x y
        output.WriteLine(sprintf "%b" result)
        // Assert
        result |> should equal expected

    [<Theory>]
    [<InlineData(1,2,false)>]
    [<InlineData(4,3,true)>]
    member this.``Decrease test`` (x, y, expected) =
        // Arrange
        // Act
        let result = decrease x y
        output.WriteLine(sprintf "%b" result)
        // Assert
        result |> should equal expected
