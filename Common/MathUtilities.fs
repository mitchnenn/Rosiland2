module MathUtilities

let factorial n = [1..n] |> List.reduce (*)
