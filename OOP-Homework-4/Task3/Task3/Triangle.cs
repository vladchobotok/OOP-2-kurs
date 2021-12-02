using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    abstract class Triangle
    {
        public virtual string calculatePerimeter(double a, double b, double angle)
        {
            double c = Math.Sqrt(a * a + b * b - 2 * a * b * Math.Cos(angle));
            double perimeter = a + b + c;
            return perimeter.ToString();
        }
        public virtual string calculateSquare(double a, double b, double angle)
        {
            double square = a * b * Math.Sin(angle * (180.0 / Math.PI)) / 2;
            return square.ToString();
        }
    }
}
