// Alternative C# style before records and richer pattern matching.
// Тут ми використовуємо класичну об'єктно-орієнтовану ієрархію типів.

using System;
using System.Collections.Generic;

namespace DiscriminatedUnions.Legacy
{
    public abstract class Shape
    {
        public abstract double Area();
        public abstract string Describe();
    }

    public class Circle : Shape
    {
        public double Radius { get; }

        public Circle(double radius) => Radius = radius;

        public override double Area() => Math.PI * Radius * Radius;
        public override string Describe() => $"Circle with radius {Radius}";
    }

    public class Rectangle : Shape
    {
        public double Width { get; }
        public double Height { get; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double Area() => Width * Height;
        public override string Describe() => $"Rectangle {Width}x{Height}";
    }

    public class Triangle : Shape
    {
        public double BaseLength { get; }
        public double Height { get; }

        public Triangle(double baseLength, double height)
        {
            BaseLength = baseLength;
            Height = height;
        }

        public override double Area() => BaseLength * Height / 2.0;
        public override string Describe() => $"Triangle base {BaseLength} height {Height}";
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
