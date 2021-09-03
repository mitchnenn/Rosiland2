module StringUtilityTests

open StringUtilities
open Xunit
open Xunit.Abstractions
open FsUnit

type ``String to character array tests`` (output:ITestOutputHelper) =
    
    [<Fact>]
    member this.``Char count test`` () =
        // Arrange
        let aString = "ACCGCCTT"
        // Act
        let result = aString |> charCount 'C'
        output.WriteLine(sprintf "%i" result)
        // Assert
        result |> should equal 4
        