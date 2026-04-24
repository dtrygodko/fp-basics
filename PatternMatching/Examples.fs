// PatternMatching в F#
// Простий приклад з типами і умовами, а також більш складне розпізнавання даних.

type Shape =
    | Circle of radius: float
    | Rectangle of width: float * height: float
    | Triangle of baseLength: float * height: float
    | Point

let area shape =
    match shape with
    | Circle radius when radius > 0.0 -> System.Math.PI * radius * radius
    | Rectangle (w, h) when w > 0.0 && h > 0.0 -> w * h
    | Triangle (b, h) when b > 0.0 && h > 0.0 -> b * h / 2.0
    | Point -> 0.0

let describe = function
    | Circle radius -> sprintf "Коло з радіусом %.2f" radius
    | Rectangle (w, h) -> sprintf "Прямокутник %.2fx%.2f" w h
    | Triangle (b, h) -> sprintf "Трикутник з основою %.2f та висотою %.2f" b h
    | Point -> "Точка"

let classifyNumber value =
    match value with
    | 0 -> "Нуль"
    | 1 -> "Одиниця"
    | 2 | 3 | 5 | 7 | 11 -> "Мале просте число"
    | n when n < 0 -> "Від'ємне число"
    | n when n % 2 = 0 -> "Чітне число"
    | _ -> "Непарне число"

let describePair pair =
    match pair with
    | (0, 0) -> "Початок координат"
    | (x, 0) -> sprintf "На осі X: %d" x
    | (0, y) -> sprintf "На осі Y: %d" y
    | (x, y) -> sprintf "Точка (%d, %d)" x y

let printOption value =
    match value with
    | Some v when v > 10 -> sprintf "Велике значення %d" v
    | Some v -> sprintf "Мале або середнє значення %d" v
    | None -> "Немає значення"

[<EntryPoint>]
let main _ =
    let shapes = [
        Circle 3.0
        Rectangle (4.0, 5.0)
        Triangle (6.0, 4.0)
        Point
    ]

    shapes |> List.iter (fun shape ->
        printfn "%s -> площа = %.2f" (describe shape) (area shape))

    [ -1; 0; 2; 4; 13 ]
    |> List.iter (fun n -> printfn "%d -> %s" n (classifyNumber n))

    [ (0, 0); (5, 0); (0, 3); (2, 3) ]
    |> List.iter (fun p -> printfn "%s" (describePair p))

    [ Some 5; Some 20; None ]
    |> List.iter (fun o -> printfn "%s" (printOption o))

    0
