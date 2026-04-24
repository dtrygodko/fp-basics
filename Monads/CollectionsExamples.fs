module Monads.CollectionsExamples

// Приклади роботи з колекціями в F# з використанням bind (>>=)
// Bind для колекцій працює як flatMap: перетворює кожен елемент на колекцію і "розплющує" результат

let numbers = [1; 2; 3; 4; 5]

// Приклад 1: Отримати всі пари (x, y) де x < y
let pairsExample () =
    let pairs =
        numbers
        |> List.collect (fun x ->
            numbers
            |> List.filter (fun y -> x < y)
            |> List.map (fun y -> (x, y))
        )
    printfn "Пари (x, y) де x < y: %A" pairs

// Те ж саме з використанням bind
let pairsWithBindExample () =
    let pairs =
        numbers
        >>= fun x ->
            numbers
            |> List.filter (fun y -> x < y)
            |> List.map (fun y -> (x, y))
    printfn "Пари з bind: %A" pairs

// Приклад 2: Генерація комбінацій
let colors = ["червоний"; "зелений"; "синій"]
let sizes = ["S"; "M"; "L"]

let combinationsExample () =
    let combinations =
        colors
        >>= fun color ->
            sizes
            >>= fun size ->
                [sprintf "%s %s" color size]
    printfn "Комбінації: %A" combinations

// Приклад 3: Фільтрація та трансформація вкладених колекцій
let nestedLists = [[1; 2]; [3; 4; 5]; [6]]

let flattenAndFilterExample () =
    let result =
        nestedLists
        >>= fun innerList ->
            innerList
            |> List.filter (fun x -> x % 2 = 0)
            |> List.map (fun x -> x * 10)
    printfn "Відфільтровані та помножені парні числа: %A" result

// Приклад 4: Ієрархічна структура (дерево)
type Tree<'T> =
    | Leaf of 'T
    | Node of Tree<'T> list

let tree = Node [Leaf 1; Node [Leaf 2; Leaf 3]; Leaf 4]

let rec flattenTree tree =
    match tree with
    | Leaf value -> [value]
    | Node children -> children >>= flattenTree

let treeFlattenExample () =
    let flattened = flattenTree tree
    printfn "Розплющене дерево: %A" flattened

// Приклад 5: Асинхронна обробка колекцій
let asyncWork x = async {
    do! Async.Sleep 10  // Симуляція асинхронної роботи
    // Повертаємо масив факторів числа
    return [for i in 1 .. x do if x % i = 0 then yield i]
}

let asyncCollectionsExample () =
    let asyncResults =
        numbers
        >>= fun x ->
            asyncWork x
            |> Async.RunSynchronously
    printfn "Асинхронні фактори: %A" asyncResults

let runCollectionsExamples () =
    pairsExample()
    pairsWithBindExample()
    combinationsExample()
    flattenAndFilterExample()
    treeFlattenExample()
    asyncCollectionsExample()
