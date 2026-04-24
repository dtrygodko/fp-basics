// Discriminated union in F#
// Це тип, який може бути одним із кількох варіантів, кожен з яких може мати свої дані.

type Shape =
    | Circle of radius: float
    | Rectangle of width: float * height: float
    | Triangle of baseLength: float * height: float

let area shape =
    match shape with
    | Circle radius -> System.Math.PI * radius * radius
    | Rectangle (width, height) -> width * height
    | Triangle (baseLength, height) -> baseLength * height / 2.0

let describe shape =
    match shape with
    | Circle radius -> sprintf "Circle with radius %.2f" radius
    | Rectangle (width, height) -> sprintf "Rectangle %.2fx%.2f" width height
    | Triangle (baseLength, height) -> sprintf "Triangle base %.2f height %.2f" baseLength height

[<EntryPoint>]
let main _ =
    let shapes = [
        Circle 3.0
        Rectangle (4.0, 5.0)
        Triangle (6.0, 4.0)
    ]

    shapes
    |> List.iter (fun shape ->
        printfn "%s -> area = %.2f" (describe shape) (area shape))

    0
