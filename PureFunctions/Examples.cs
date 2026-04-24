using System;
using System.Collections.Generic;

namespace PureFunctions
{
    public static class Examples
    {
        // Чиста функція: результат залежить лише від аргументів.
        public static int Add(int a, int b) => a + b;

        // Чиста функція, без змін стану, без зовнішніх ресурсів.
        public static int SumList(IReadOnlyList<int> values)
        {
            return values.Sum();
        }

        // Функція, що може повернути null замість побічного ефекту або аварійного стану.
        public static int? TryParseInt(string text)
        {
            if (int.TryParse(text, out var value))
                return value;
            return null;
        }

        // Функція, яка може кинути виключення, але сигнатура цього не показує.
        public static int ParsePositiveId(string value)
        {
            if (!int.TryParse(value, out var id))
            {
                throw new FormatException("Неправильний формат ідентифікатора");
            }
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "ID має бути додатнім");
            }
            return id;
        }

        // Чиста функція для потоків: створює новий рядок, не змінюючи глобальний стан.
        public static string ReverseString(string input)
        {
            var chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        // Функція, яка може повернути null, хоча сигнатура виглядає як простий string.
        // Це приклад небезпечного API, який не вказує побічні ефекти.
        public static string GetEnvironmentValue(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }

        public static void ExampleUsage()
        {
            var total = Add(3, 7);
            var sum = SumList(new[] { 1, 2, 3 });
            var maybeValue = TryParseInt("42");
            var envValue = GetEnvironmentValue("PATH");
            var reversed = ReverseString("Hello");
            var id = ParsePositiveId("10");
            Console.WriteLine($"Total: {total}, Sum: {sum}, Maybe: {maybeValue}, Env: {envValue}, Reversed: {reversed}, ID: {id}");
        }
    }
}
