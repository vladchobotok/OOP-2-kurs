using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    class OtherTriangle : Triangle
    {
        public override string calculatePerimeter(double a, double b, double angle)
        {
            double c = Math.Sqrt(a * a + b * b - 2 * a * b * Math.Cos(angle * (Math.PI / 180.0)));
            double perimeter = a + b + c;
            return perimeter.ToString();
        }
        public override string calculateSquare(double a, double b, double angle)
        {
            double square = a * b * Math.Sin(angle * (Math.PI / 180.0)) / 2;
            return square.ToString();
        }
    }
}
