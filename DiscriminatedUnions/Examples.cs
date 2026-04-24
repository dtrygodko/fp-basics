// Simulating discriminated unions in C# using abstract base types and records.
// Це дозволяє описати значення, яке може бути одним із кількох різних варіантів.

using System;
using System.Collections.Generic;

namespace DiscriminatedUnions
{
    public abstract record Shape;

    public record Circle(double Radius) : Shape;
    public record Rectangle(double Width, double Height) : Shape;
    public record Triangle(double BaseLength, double Height) : Shape;

    public static class ShapeExtensions
    {
        public static double Area(this Shape shape) => shape switch
        {
            Circle c => Math.PI * c.Radius * c.Radius,
            Rectangle r => r.Width * r.Height,
            Triangle t => t.BaseLength * t.Height / 2.0,
            _ => throw new ArgumentException("Unknown shape", nameof(shape))
        };

        public static string Describe(this Shape shape) => shape switch
        {
            Circle c => $"Circle with radius {c.Radius}",
            Rectangle r => $"Rectangle {r.Width}x{r.Height}",
            Triangle t => $"Triangle base {t.BaseLength} height {t.Height}",
            _ => "Unknown shape"
        };
    }

    class Program
    {
        static void Main()
        {
            var shapes = new List<Shape>
            {
                new Circle(3.0),
                new Rectangle(4.0, 5.0),
                new Triangle(6.0, 4.0)
            };

            foreach (var shape in shapes)
            {
                Console.WriteLine($"{shape.Describe()} -> area = {shape.Area():F2}");
            }
        }
    }
}
