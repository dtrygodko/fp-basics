using System;
using System.Collections.Generic;
using System.Linq;

namespace Monads.CollectionsExamples
{
    // Приклади роботи з колекціями в C# з використанням SelectMany (LINQ)
    // SelectMany працює як flatMap: перетворює кожен елемент на колекцію і "розплющує" результат

    class Program
    {
        static readonly List<int> numbers = new() { 1, 2, 3, 4, 5 };

        // Приклад 1: Отримати всі пари (x, y) де x < y
        static void PairsExample()
        {
            var pairs = numbers
                .SelectMany(x => numbers
                    .Where(y => x < y)
                    .Select(y => (x, y)))
                .ToList();

            Console.WriteLine($"Пари (x, y) де x < y: {string.Join(", ", pairs)}");
        }

        // Приклад 2: Генерація комбінацій
        static readonly List<string> colors = new() { "червоний", "зелений", "синій" };
        static readonly List<string> sizes = new() { "S", "M", "L" };

        static void CombinationsExample()
        {
            var combinations = colors
                .SelectMany(color => sizes
                    .Select(size => $"{color} {size}"))
                .ToList();

            Console.WriteLine($"Комбінації: {string.Join(", ", combinations)}");
        }

        // Приклад 3: Фільтрація та трансформація вкладених колекцій
        static readonly List<List<int>> nestedLists = new()
        {
            new() { 1, 2 },
            new() { 3, 4, 5 },
            new() { 6 }
        };

        static void FlattenAndFilterExample()
        {
            var result = nestedLists
                .SelectMany(innerList => innerList
                    .Where(x => x % 2 == 0)
                    .Select(x => x * 10))
                .ToList();

            Console.WriteLine($"Відфільтровані та помножені парні числа: {string.Join(", ", result)}");
        }

        // Приклад 4: Ієрархічна структура (дерево)
        abstract class Tree<T>
        {
            public abstract IEnumerable<T> Flatten();
        }

        class Leaf<T> : Tree<T>
        {
            public T Value { get; }
            public Leaf(T value) => Value = value;

            public override IEnumerable<T> Flatten() => new[] { Value };
        }

        class Node<T> : Tree<T>
        {
            public List<Tree<T>> Children { get; }
            public Node(List<Tree<T>> children) => Children = children;

            public override IEnumerable<T> Flatten() =>
                Children.SelectMany(child => child.Flatten());
        }

        static void TreeFlattenExample()
        {
            var tree = new Node<int>(new List<Tree<int>>
            {
                new Leaf<int>(1),
                new Node<int>(new List<Tree<int>> { new Leaf<int>(2), new Leaf<int>(3) }),
                new Leaf<int>(4)
            });

            var flattened = tree.Flatten().ToList();
            Console.WriteLine($"Розплющене дерево: {string.Join(", ", flattened)}");
        }

        // Приклад 5: Асинхронна обробка колекцій
        static async Task AsyncCollectionsExample()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            var asyncResults = await Task.WhenAll(
                numbers.Select(async x =>
                {
                    await Task.Delay(10); // Симуляція асинхронної роботи
                    return x * x;
                })
            );

            var flattened = asyncResults
                .SelectMany(result => new[] { result }) // Тут просто для демонстрації
                .ToList();

            Console.WriteLine($"Асинхронні результати: {string.Join(", ", flattened)}");
        }

        static async Task Main()
        {
            PairsExample();
            CombinationsExample();
            FlattenAndFilterExample();
            TreeFlattenExample();
            await AsyncCollectionsExample();
        }
    }
}
