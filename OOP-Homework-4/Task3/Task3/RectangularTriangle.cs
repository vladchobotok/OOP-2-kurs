using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    class RectangularTriangle : Triangle
    {
        public override string calculatePerimeter(double a, double b, double angle)
        {
            double c = Math.Sqrt(a * a + b * b);
            double perimeter = a + b + c;
            return perimeter.ToString();
        }
        public override string calculateSquare(double a, double b, double angle)
        {
            double square = a * b / 2;
            return square.ToString();
        }
    }
}
