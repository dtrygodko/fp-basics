public class Examples
{
    public static void Main()
    {
        var x = 5;

        var list = new List<int> { 1, 2, 3, 4, 5 }; // зараз можна і [1, 2, 3, 4, 5]

        var doubled =
            list
            .Where(n => n > 1)
            .Select(n => n * 2)
            .ToList();

        string? name = null;

        if (name != null)
        {
            Console.WriteLine(name.Length);
        }

        var point = (x: 3, y: 4);
        Console.WriteLine($"Point: {point.x}, {point.y}");

        var swapped = (point.y, point.x);
        Console.WriteLine($"Swapped: {swapped.Item1}, {swapped.Item2}");
    }

    public int Add(int a, int b) => a + b;
}

public record User(string Name);
