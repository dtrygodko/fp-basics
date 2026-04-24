module Examples

let main() =
    let x = 5

    let list = [1; 2; 3; 4; 5]

    let doubled =
        list
        |> List.filter (fun x -> x > 1)
        |> List.map (fun x -> x * 2)

    let name: string option = None

    if name.IsSome then
        printfn "%d" name.Value.Length
    else
        ()

    let point = 3, 4
    let xCoord, yCoord = point
    printfn "Point: %d, %d" xCoord yCoord

    let swapped = yCoord, xCoord
    printfn "Swapped: %d, %d" (fst swapped) (snd swapped)

    let pair = "hello", 42
    let message, number = pair
    printfn "Pair: %s = %d" message number

let add a b = a + b
let add (a, b) = a + b

type User = { Name: string }
