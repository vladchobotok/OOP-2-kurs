using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    class IsoscelesTriangle : Triangle
    {
        public override string calculatePerimeter(double a, double b, double angle)
        {
            double c = a * Math.Sqrt(2 * (1 - Math.Cos(angle * (Math.PI / 180.0))));
            double perimeter = 2 * a + c;
            return perimeter.ToString();
        }
        public override string calculateSquare(double a, double b, double angle)
        {
            double square = a * a * Math.Sin(angle * (Math.PI / 180.0)) / 2;
            return square.ToString();
        }
    }
}
