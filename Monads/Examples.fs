module Monads.Examples

// F# часто використовує вбудовані монади: option, Result, async.
// Тут показано, як вони працюють, а також практичний приклад AsyncOption.
// Також додано власні оператори для зручності роботи з монадами.

let tryDivide x y : int option =
    if y = 0 then None else Some (x / y)

// Власні оператори для Option
let (>>=) m f = Option.bind f m
let (<!>) f m = Option.map f m

let optionExample () =
    let result: int option =
        Some 10
        >>= tryDivide 0
        >>= tryDivide 2
        >>= tryDivide 5
    match result with
    | Some value -> sprintf "Результат option: %d" value
    | None -> "Неможливо поділити"

let parsePositiveInt (text: string) : Result<int, string> =
    match System.Int32.TryParse(text) with
    | true, value when value > 0 -> Ok value
    | true, _ -> Error "Число має бути додатнім"
    | false, _ -> Error "Не вдалося розпарсити число"

// Власні оператори для Result
let (>>=!) m f = Result.bind f m
let (<!>) f m = Result.map f m

let resultExample () =
    let computation =
        parsePositiveInt "12"
        >>=! parsePositiveInt << string << (-) 2
        <!> (*) 3

    match computation with
    | Ok value -> sprintf "Результат Result: %d" value
    | Error msg -> sprintf "Помилка Result: %s" msg

let asyncWork x = async {
    do! Async.Sleep 100
    return x * x
}

let asyncExample () =
    async {
        let! a = asyncWork 3
        let! b = asyncWork 4
        return sprintf "Результат async: %d" (a + b)
    }

// Практичний AsyncOption: комбінація Async і option.
type AsyncOption<'T> = Async<option<'T>>

let asyncReturn x : AsyncOption<_> = async { return Some x }
let asyncZero<'T> : AsyncOption<'T> = async { return None }

let asyncBind (computation: AsyncOption<'T>) (binder: 'T -> AsyncOption<'U>) : AsyncOption<'U> = async {
    let! result = computation
    match result with
    | Some value -> return! binder value
    | None -> return None
}

let asyncMap (mapper: 'T -> 'U) (computation: AsyncOption<'T>) : AsyncOption<'U> = async {
    let! result = computation
    return Option.map mapper result
}

// Власні оператори для AsyncOption
let (>>=!!) m f = asyncBind m f
let (<!>!) f m = asyncMap f m

type AsyncOptionBuilder() =
    member _.Return(value) = asyncReturn value
    member _.Bind(computation, binder) = asyncBind computation binder
    member _.ReturnFrom(computation) = computation
    member _.Zero() = asyncZero

let asyncOption = AsyncOptionBuilder()

let tryDivideAsync x y : AsyncOption<int> = async {
    do! Async.Sleep 50
    if y = 0 then return None else return Some (x / y)
}

let asyncOptionExample () =
    async {
        let! result =
            asyncOption {
                let! a = tryDivideAsync 10 2
                let! b = tryDivideAsync a 5
                return a + b
            }

        return
            match result with
            | Some value -> sprintf "Результат AsyncOption: %d" value
            | None -> "Неможливо поділити асинхронно"
    }

let asyncOptionWithOperatorsExample () =
    async {
        let! result =
            asyncReturn 10
            >>=!! tryDivideAsync 2
            >>=!! tryDivideAsync 5
            <!>! id  // ідентична функція, щоб не змінювати значення

        return
            match result with
            | Some value -> sprintf "Результат AsyncOption з операторами: %d" value
            | None -> "Неможливо поділити асинхронно"
    }

let validateEven x : option<int> =
    Some x
    |> Option.bind (fun value -> if value % 2 = 0 then Some (value / 2) else None)

let tryDivideIfEven x : Result<int, string> =
    match validateEven x with
    | Some value -> Ok value
    | None -> Error "Число не парне"

let railwayExample () =
    let computation =
        parsePositiveInt "20"
        |> Result.bind tryDivideIfEven
        |> Result.map (fun x -> x + 5)

    match computation with
    | Ok value -> sprintf "Результат Railway: %d" value
    | Error msg -> sprintf "Помилка Railway: %s" msg

let parsePositiveAsync (text: string) : AsyncOption<int> =
    async {
        match parsePositiveInt text with
        | Ok value -> return Some value
        | Error _ -> return None
    }

let railwayAsyncExample () =
    async {
        let! result =
            asyncOption {
                let! value = parsePositiveAsync "20"
                let! half = tryDivideAsync value 2
                return half
            }
            |> asyncMap ((+) 1)

        return
            match result with
            | Some value -> sprintf "Результат Railway AsyncOption: %d" value
            | None -> "Помилка Railway AsyncOption"
    }

let runExamples () =
    printfn "%s" (optionExample())
    printfn "%s" (resultExample())
    asyncExample () |> Async.RunSynchronously |> printfn "%s"
    asyncOptionExample () |> Async.RunSynchronously |> printfn "%s"
    asyncOptionWithOperatorsExample () |> Async.RunSynchronously |> printfn "%s"
    railwayExample () |> printfn "%s"
    railwayAsyncExample () |> Async.RunSynchronously |> printfn "%s"
