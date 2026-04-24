using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monads.Examples
{
    // У C# немає вбудованих монадів у термінах F#, але є практичні аналоги.
    // - Option: Nullable<T> або власний Maybe<T>
    // - Result: власний тип для обробки успіху/помилки
    // - Async: Task<T> та async/await
    // C# не підтримує створення власних операторів, тому використовуємо методи розширення.

    record Result<T, E>(T Value, E Error, bool IsOk)
    {
        public static Result<T, E> Ok(T value) => new(value, default!, true);
        public static Result<T, E> Error(E error) => new(default!, error, false);
    }

    static class ResultExtensions
    {
        public static Result<U, E> Bind<T, U, E>(this Result<T, E> result, Func<T, Result<U, E>> func) =>
            result.IsOk ? func(result.Value) : Result<U, E>.Error(result.Error);

        public static Result<U, E> Map<T, U, E>(this Result<T, E> result, Func<T, U> func) =>
            result.IsOk ? Result<U, E>.Ok(func(result.Value)) : Result<U, E>.Error(result.Error);
    }

    record Maybe<T>(T Value, bool HasValue)
    {
        public static Maybe<T> Some(T value) => new(value, true);
        public static Maybe<T> None => new(default!, false);
    }

    static class MaybeExtensions
    {
        public static Maybe<U> Bind<T, U>(this Maybe<T> maybe, Func<T, Maybe<U>> func) =>
            maybe.HasValue ? func(maybe.Value) : Maybe<U>.None;

        public static Maybe<U> Map<T, U>(this Maybe<T> maybe, Func<T, U> func) =>
            maybe.HasValue ? Maybe<U>.Some(func(maybe.Value)) : Maybe<U>.None;

        public static async Task<Maybe<U>> BindAsync<T, U>(this Task<Maybe<T>> maybeTask, Func<T, Task<Maybe<U>>> func)
        {
            var maybe = await maybeTask;
            return maybe.HasValue ? await func(maybe.Value) : Maybe<U>.None;
        }

        public static async Task<Maybe<U>> MapAsync<T, U>(this Task<Maybe<T>> maybeTask, Func<T, U> func)
        {
            var maybe = await maybeTask;
            return maybe.HasValue ? Maybe<U>.Some(func(maybe.Value)) : Maybe<U>.None;
        }
    }

    class Program
    {
        static int? TryDivide(int x, int y) => y == 0 ? null : x / y;

        static string OptionExample()
        {
            var result = TryDivide(10, 2)
                is int value ? TryDivide(value, 5) : null;

            return result.HasValue
                ? $"Результат option: {result.Value}"
                : "Неможливо поділити";
        }

        static Result<int, string> ParsePositiveInt(string text)
        {
            return int.TryParse(text, out var value)
                ? value > 0
                    ? Result<int, string>.Ok(value)
                    : Result<int, string>.Error("Число має бути додатнім")
                : Result<int, string>.Error("Не вдалося розпарсити число");
        }

        static string ResultExample()
        {
            var computation = ParsePositiveInt("12")
                .Bind(x => ParsePositiveInt((x - 2).ToString()))
                .Map(x => x * 3);

            return computation.IsOk
                ? $"Результат Result: {computation.Value}"
                : $"Помилка Result: {computation.Error}";
        }

        static async Task<string> AsyncExample()
        {
            async Task<int> AsyncWork(int x)
            {
                await Task.Delay(100);
                return x * x;
            }

            var a = await AsyncWork(3);
            var b = await AsyncWork(4);
            return $"Результат async: {a + b}";
        }

        static Task<Maybe<int>> TryDivideAsync(int x, int y) =>
            y == 0 ? Task.FromResult(Maybe<int>.None) : Task.FromResult(Maybe<int>.Some(x / y));

        static async Task<string> AsyncOptionExample()
        {
            var result = await TryDivideAsync(10, 2)
                .BindAsync(x => TryDivideAsync(x, 5));

            return result.HasValue
                ? $"Результат AsyncOption: {result.Value}"
                : "Неможливо поділити асинхронно";
        }

        static Result<int, string> ValidateEven(int x) =>
            x % 2 == 0
                ? Result<int, string>.Ok(x / 2)
                : Result<int, string>.Error("Число не парне");

        static string RailwayExample()
        {
            var computation = ParsePositiveInt("20")
                .Bind(ValidateEven)
                .Map(x => x + 5);

            return computation.IsOk
                ? $"Результат Railway: {computation.Value}"
                : $"Помилка Railway: {computation.Error}";
        }

        static Task<Maybe<int>> ValidateEvenAsync(int x) =>
            x % 2 == 0
                ? Task.FromResult(Maybe<int>.Some(x / 2))
                : Task.FromResult(Maybe<int>.None);

        static async Task<string> RailwayAsyncExample()
        {
            var result = await TryDivideAsync(20, 2)
                .BindAsync(x => ValidateEvenAsync(x))
                .MapAsync(x => x + 1);

            return result.HasValue
                ? $"Результат Railway AsyncOption: {result.Value}"
                : "Помилка Railway AsyncOption";
        }

        static async Task Main()
        {
            Console.WriteLine(OptionExample());
            Console.WriteLine(ResultExample());
            Console.WriteLine(await AsyncExample());
            Console.WriteLine(await AsyncOptionExample());
            Console.WriteLine(RailwayExample());
            Console.WriteLine(await RailwayAsyncExample());
        }
    }
}
