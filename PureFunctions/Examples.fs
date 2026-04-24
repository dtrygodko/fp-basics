module PureFunctions.Examples

// Чиста функція: завжди однаковий результат, без змін стану.
let add x y = x + y

// Чиста функція без побічних ефектів та без мутації — вона безпечна для потоків.
let sumList (values: int list) =
    values |> List.sum

// Функція, що може повернути None замість побічного ефекту або аварійного стану.
let tryParseInt (text: string) : int option =
    match System.Int32.TryParse(text) with
    | true, value -> Some value
    | false, _ -> None

// Приклад: функція має сигнатуру, яка не каже про лавку побічних ефектів.
// У F# можна зробити краще, повертаючи option або Result для безпечнішої обробки.
let parsePositiveId (value: string) : int =
    let id = System.Int32.Parse(value)
    if id <= 0 then failwith "ID має бути додатнім"
    id

// Чиста функція трансформації: переворотить рядок без змін глобального стану.
let reverseString (text: string) : string =
    text.ToCharArray()
    |> Array.rev
    |> System.String

// Функція з зовнішнім ефектом, але ми явно сигналізуємо про можливий провал через Result.
let readTextFile (path: string) : Result<string, string> =
    try
        let text = System.IO.File.ReadAllText(path)
        Ok text
    with ex -> Error ex.Message

let exampleUsage () =
    let total = add 3 7
    let sum = sumList [1; 2; 3]
    let maybeValue = tryParseInt "42"
    let fileResult = readTextFile "config.txt"
    let reversed = reverseString "Hello"
    // parsePositiveId може кинути виняток, хоча в сигнатурі це не видно.
    let id = parsePositiveId "10"
    total, sum, maybeValue, fileResult, reversed, id
