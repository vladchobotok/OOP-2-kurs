using System;
using System.Collections.Generic;
using System.Text;

namespace Task2
{
    class Triangle : Figure
    {
        private double a;
        private double b;
        private double c;
        public override double calculatePerimeter()
        {
            return a + b + c;
        }
        public override double calculateSquare()
        {
            double halfPerimeter = (a + b + c) / 2;
            return (Math.Sqrt(halfPerimeter * (halfPerimeter - a) * (halfPerimeter - b) * (halfPerimeter - c)));
        }

        public Triangle(double _a, double _b, double _c)
        {
            a = _a;
            b = _b;
            c = _c;
        }
    }
}
