namespace WorkForcs2;
using System;
using System.Transactions;

class MainProgram
{
    static void Main(string[] args)
    {
        Random random = new Random();
        Shape[] shapes = new Shape[10];
        InitializeShapes(random, shapes);
        ShowResult(shapes);
    }

    private static void ShowResult(Shape[] shapes)
    {
        double sumOfAreas = 0;
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Here is a " + shapes[i].GetType());
            sumOfAreas += shapes[i].GetArea();
        }
        Console.WriteLine("The Whole Areas of the Shapes are " + sumOfAreas);
    }

    private static void InitializeShapes(Random random, Shape[] shapes)
    {
        int type;
        for (int i = 0; i < 10; i++)
        {
            type = random.Next(0, 4);
            double[] edges;

            switch (type)
            {
                case 0:
                    edges = new double[1];
                    edges[0] = random.NextDouble() * random.Next(10, 20);
                    break;

                case 1:
                    edges = new double[4];
                    edges[0] = random.NextDouble() * random.Next(10, 20);
                    edges[1] = random.NextDouble() * random.Next(10, 20);
                    edges[2] = edges[0];
                    edges[3] = edges[1];
                    break;

                case 2:
                    edges = new double[4];
                    edges[0] = random.NextDouble() * random.Next(10, 20);
                    edges[1] = edges[0];
                    edges[2] = edges[0];
                    edges[3] = edges[0];
                    break;

                case 3:
                    edges = new double[3];
                    edges[0] = random.NextDouble() * random.Next(10, 20);
                    edges[1] = random.NextDouble() * random.Next(10, 20);
                    edges[2] = random.NextDouble() * random.Next(10, 20);
                    break;

                default:
                    edges = new double[1];
                    break;
            }

            Shape shape = type switch
            {
                0 => new Circle(edges),
                1 => new Rectangle(edges),
                2 => new Square(edges),
                3 => new Triangle(edges),
                _ => new Triangle(edges)
            };
            if (shape.IsValid())
            {
                shapes[i] = shape;
            }
            else
            {
                Console.WriteLine("Invalid shape generated, regenerating...");
                i--;
            }
        }
    }
}