using System;
using System.Collections.Generic;

// Pattern matching у C#
// Простий приклад із числами, кортежами, умовами та властивостями об'єктів.

record Shape;

record Circle(double Radius) : Shape;
record Rectangle(double Width, double Height) : Shape;
record Triangle(double BaseLength, double Height) : Shape;
record Point() : Shape;

static double CalculateArea(Shape shape) => shape switch
{
    Circle { Radius: > 0 } c => Math.PI * c.Radius * c.Radius,
    Rectangle { Width: > 0, Height: > 0 } r => r.Width * r.Height,
    Triangle { BaseLength: > 0, Height: > 0 } t => t.BaseLength * t.Height / 2,
    Point _ => 0,
    _ => throw new ArgumentException("Невірна форма")
};

static string Describe(Shape shape) => shape switch
{
    Circle c => $"Коло з радіусом {c.Radius:F2}",
    Rectangle r => $"Прямокутник {r.Width:F2}x{r.Height:F2}",
    Triangle t => $"Трикутник з основою {t.BaseLength:F2} та висотою {t.Height:F2}",
    Point => "Точка",
    _ => "Невідома форма"
};

static string ClassifyNumber(int value) => value switch
{
    0 => "Нуль",
    1 => "Одиниця",
    2 or 3 or 5 or 7 or 11 => "Мале просте число",
    < 0 => "Від'ємне число",
    _ when value % 2 == 0 => "Чітне число",
    _ => "Непарне число"
};

static string DescribePair((int X, int Y) pair) => pair switch
{
    (0, 0) => "Початок координат",
    (var x, 0) => $"На осі X: {x}",
    (0, var y) => $"На осі Y: {y}",
    (var x, var y) => $"Точка ({x}, {y})"
};

static string PrintOption(int? value) => value switch
{
    int v when v > 10 => $"Велике значення {v}",
    int v => $"Мале або середнє значення {v}",
    null => "Немає значення"
};

Console.WriteLine("=== Pattern Matching C# ===");

var shapes = new List<Shape>
{
    new Circle(3),
    new Rectangle(4, 5),
    new Triangle(6, 4),
    new Point()
};

foreach (var shape in shapes)
{
    Console.WriteLine($"{Describe(shape)} -> площа = {CalculateArea(shape):F2}");
}

foreach (var value in new[] { -1, 0, 2, 4, 13 })
{
    Console.WriteLine($"{value} -> {ClassifyNumber(value)}");
}

foreach (var pair in new[] { (0, 0), (5, 0), (0, 3), (2, 3) })
{
    Console.WriteLine(DescribePair(pair));
}

foreach (var option in new int?[] { 5, 20, null })
{
    Console.WriteLine(PrintOption(option));
}
